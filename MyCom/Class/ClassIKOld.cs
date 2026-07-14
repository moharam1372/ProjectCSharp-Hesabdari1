using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCom.Class
{
    public static class ClassIkOld
    {



        private static readonly string _connectionString = "Server=kvserver;Database=IKCheck;User Id=sa;Password=Moaz1370110;TrustServerCertificate=true;";

        //public OldSchoolDatabase(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        public static List<MultimediaGroupedDto> GetGroupedDataOld()
        {
            var results = new List<MultimediaGroupedDto>();

            string query = @"
        SELECT
            MultimediaHeaders.Num,
            MultimediaHeaders.NamePersian,
            MultiGroup1.Title AS Country,
            MultiGroup2.Title AS ""Ser|Sin"",
            COUNT(MultimediaListPersonnels.Id) AS PersonnelCount,
            CASE 
                WHEN EXISTS (
                    SELECT 1 
                    FROM dbo.MultiGenreLists MGL 
                    WHERE MGL.MultimediaHeaderId = MultimediaHeaders.Id 
                    AND MGL.MultiGenreId = 26
                    AND (MGL.[Delete] = 0 OR MGL.[Delete] IS NULL)
                ) THEN 1 
                ELSE 0 
            END AS IsGenre26,
            CASE 
                WHEN EXISTS (
                    SELECT 1 
                    FROM dbo.MultiGenreLists MGL 
                    WHERE MGL.MultimediaHeaderId = MultimediaHeaders.Id 
                    AND (MGL.MultiGenreId = 22 OR MGL.MultiGenreId = 13)
                    AND (MGL.[Delete] = 0 OR MGL.[Delete] IS NULL)
                ) THEN 1 
                ELSE 0 
            END AS IsGenre22Or13
        FROM dbo.MultimediaDetails
        INNER JOIN dbo.MultimediaHeaders
            ON MultimediaDetails.MultimediaHeaderId = MultimediaHeaders.Id
        INNER JOIN dbo.MultimediaListPersonnels
            ON MultimediaListPersonnels.MultimediaDetailId = MultimediaDetails.Id
        INNER JOIN dbo.MultiGroup1
            ON MultimediaHeaders.MultiGroup1Id = MultiGroup1.Id
        INNER JOIN dbo.MultiGroup2
            ON MultimediaHeaders.MultiGroup2Id = MultiGroup2.Id
        GROUP BY 
            MultimediaHeaders.Num,
            MultimediaHeaders.NamePersian,
            MultiGroup1.Title,
            MultiGroup2.Title,
            MultimediaHeaders.Id
        ORDER BY 
            MultimediaHeaders.Num";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new MultimediaGroupedDto
                            {
                                Num = reader["Num"]?.ToString() ?? "",
                                NamePersian = reader["NamePersian"]?.ToString() ?? "",
                                Country = reader["Country"]?.ToString() ?? "",
                                SerSin = reader["Ser|Sin"]?.ToString() ?? "",
                                Count = Convert.ToInt32(reader["PersonnelCount"]),
                                Animation = Convert.ToBoolean(reader["IsGenre26"]),
                                Action = Convert.ToBoolean(reader["IsGenre22Or13"])
                            };
                            results.Add(item);
                        }
                    }
                }
            }

            return results;
        }

        // مدل خروجی
        public class MultimediaGroupedDto
        {
            public string Num { get; set; }
            public string NamePersian { get; set; }
            public string Country { get; set; }
            public string SerSin { get; set; }
            public int Count { get; set; }
            public bool Animation { get; set; }
            public bool Action { get; set; }  // true اگه GenreId = 22 یا 13 داشته باشه
        }

        // برای اجرای با پارامتر (امن در برابر SQL Injection)

    }

    // کلاس مدل

}

