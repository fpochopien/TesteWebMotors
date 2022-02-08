using System.Data.SqlClient;

using TesteWebMotors.DataBase;
using TesteWebMotors.Entities;

namespace TesteWebMotors.Repositories
{
    public class AnnouncementRepository : IRepository<Announcement>
    {
        public AnnouncementRepository()
        {
            CreateTable();
        }

        #region Cria tabela caso ainda não esteja criada
        private void CreateTable()
        {
            var query = @"if not exists (select * from sys.tables where name = 'tb_AnuncioWebmotors')
                            BEGIN
                                create table tb_AnuncioWebmotors
                                (
                                ID INT identity not null, marca varchar (45) not
                                null, modelo varchar (45) not null, versao
                                varchar (45) not null, ano INT not null,
                                quilometragem INT not null, observacao text not
                                null
                                )
                            end";
            using (var connection = DataBaseManager.GetConnection())
            {
                if (connection == null)
                    return;

                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
        #endregion

        public int Create(Announcement t)
        {
            var ret = 0;

            var query = @"insert into tb_AnuncioWebmotors
                        (
                            marca,
                            modelo,
                            versao,
                            ano,
                            quilometragem,
                            observacao
                        )
                        VALUES (@field1, @field2, @field3, @field4, @field5, @field6)";
            using (var connection = DataBaseManager.GetConnection())
            {
                if (connection == null)
                    return ret;

                connection.Open();
                var transact = connection.BeginTransaction();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Transaction = transact;
                    try
                    {
                        command.Parameters.Add(new SqlParameter("@field1", t.Marca));
                        command.Parameters.Add(new SqlParameter("@field2", t.Modelo));
                        command.Parameters.Add(new SqlParameter("@field3", t.Versao));
                        command.Parameters.Add(new SqlParameter("@field4", t.Ano));
                        command.Parameters.Add(new SqlParameter("@field5", t.Quilometragem));
                        command.Parameters.Add(new SqlParameter("@field6", t.Observacao));


                        ret = command.ExecuteNonQuery();
                        transact.Commit();
                    }
                    catch (Exception ex)
                    {
                        transact.Rollback();
                        return ret;
                    }
                }
            }
            return ret;
        }

        public List<Announcement> Read(int id)
        {
            var query = @"select * from tb_AnuncioWebmotors where ID = case when @field1 = 0 then ID else @field1 end";
            Announcement announcement;
            var announcements = new List<Announcement>();
            using (var connection = DataBaseManager.GetConnection())
            {
                if (connection == null)
                    return null;

                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    try
                    {
                        command.Parameters.Add(new SqlParameter("@field1", id));

                        var dr = command.ExecuteReader();
                        if (!dr.HasRows)
                            return null;

                        while (dr.Read())
                        {
                            announcement = new Announcement()
                            {
                                Id = (int)dr["ID"],
                                Ano = (int)dr["ano"],
                                Marca = (string)dr["marca"],
                                Modelo = (string)dr["modelo"],
                                Observacao = (string)dr["Observacao"],
                                Quilometragem = (int)dr["Quilometragem"],
                                Versao = (string)dr["Versao"]
                            };

                            announcements.Add(announcement);
                        }
                        return announcements;

                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        public int Update(Announcement t)
        {

            var ret = 0;

            var query = @"update  tb_AnuncioWebmotors
                        set marca = @field2,
                        modelo = @field3,
                        versao = @field4,
                        ano = @field5,
                        quilometragem = @field6,
                        observacao = @field7
                        where ID = @field1";
            using (var connection = DataBaseManager.GetConnection())
            {
                if (connection == null)
                    return ret;

                connection.Open();
                var transact = connection.BeginTransaction("SampleTransaction");
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    try
                    {
                        command.Transaction = transact;
                        command.Parameters.Add(new SqlParameter("@field1", t.Id));
                        command.Parameters.Add(new SqlParameter("@field2", t.Marca));
                        command.Parameters.Add(new SqlParameter("@field3", t.Modelo));
                        command.Parameters.Add(new SqlParameter("@field4", t.Versao));
                        command.Parameters.Add(new SqlParameter("@field5", t.Ano));
                        command.Parameters.Add(new SqlParameter("@field6", t.Quilometragem));
                        command.Parameters.Add(new SqlParameter("@field7", t.Observacao));

                        ret = command.ExecuteNonQuery();
                        transact.Commit();
                    }
                    catch (Exception ex)
                    {
                        transact.Rollback();
                        return ret;
                    }
                }
            }
            return ret;
        }
        public int Delete(int id)
        {
            var ret = 0;

            var query = @"delete  tb_AnuncioWebmotors
                        where ID = @field1";
            using (var connection = DataBaseManager.GetConnection())
            {
                if (connection == null)
                    return ret;

                connection.Open();
                var transact = connection.BeginTransaction("SampleTransaction");
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    try
                    {
                        command.Transaction = transact;
                        command.Parameters.Add(new SqlParameter("@field1", id));

                        ret = command.ExecuteNonQuery();
                        transact.Commit();
                    }
                    catch (Exception ex)
                    {
                        transact.Rollback();
                        return ret;
                    }
                }
            }
            return ret;
        }
    }
}
