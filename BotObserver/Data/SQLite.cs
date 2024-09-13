using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotObserver.Data
{
    public class SQLite
    {
        private SQLiteConnection _conn = null;
        private SQLiteDataReader _reader = null;

        public SQLite(string databaseFileName = "")
        {
            Init(databaseFileName);
        }

        ~SQLite()
        {
            Close();
        }

        public void Init(string databaseFileName)
        {
            if (string.IsNullOrEmpty(databaseFileName)) return;

            Directory.CreateDirectory(Path.GetDirectoryName(databaseFileName));

            if (!File.Exists(databaseFileName)) Create(databaseFileName);

            Open(databaseFileName);
        }

        /// <summary>
        /// SQLite DB 생성
        /// </summary>
        /// <param name="databaseFileName">데이터베이스 파일명</param>
        /// <param name="isOpen">생성 후 오픈 여부 (디폴트: false)</param>
        public void Create(string databaseFileName, bool isOpen = false)
        {
            try
            {
                SQLiteConnection.CreateFile(databaseFileName);

                if (isOpen) Open(databaseFileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// SQLite DB 열기
        /// </summary>
        /// <param name="databaseFileName">데이터베이스 파일명</param>
        public void Open(string databaseFileName)
        {
            try
            {
                Close();

                string connectionString = GetConnectionString(databaseFileName);

                _conn = new SQLiteConnection(connectionString);

                _conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// sqlite 연결 문자열 리턴
        /// </summary>
        /// <param name="databaseFileName">데이터베이스 파일명</param>
        /// <returns>연결 문자열</returns>
        public static string GetConnectionString(string databaseFileName)
        {
            return string.Format("Data Source={0};Version=3;", databaseFileName);
        }

        /// <summary>
        /// SQLite DB 닫기
        /// </summary>
        public void Close()
        {
            CloseReader();

            try
            {
                if (_conn != null)
                {
                    _conn.Close();
                    _conn.Dispose();
                    _conn = null;

                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// SQLiteDataReader 닫기
        /// </summary>
        public void CloseReader()
        {
            try
            {
                if (_reader != null)
                {
                    _reader.Close();
                    _reader = null;
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 쿼리 실행
        /// </summary>
        /// <param name="sql">쿼리 문자열</param>
        /// <returns>영향받은 row 수</returns>
        public int ExecuteNonQuery(string sql)
        {
            try
            {
                SQLiteCommand command = new SQLiteCommand(sql, _conn);

                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 쿼리 실행 후 레코드셋 설정
        /// </summary>
        /// <param name="sql">쿼리 문자열</param>
        /// <returns>결과 row가 있으면 true, 없으면 false</returns>
        public bool ExecuteReader(string sql)
        {
            try
            {
                CloseReader();

                SQLiteCommand command = new SQLiteCommand(sql, _conn);

                _reader = command.ExecuteReader();

                return _reader.HasRows;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// SQLiteDataReader 리턴
        /// </summary>
        /// <returns>SQLiteDataReader 데이터</returns>
        public SQLiteDataReader GetReader()
        {
            return _reader;
        }

        /// <summary>
        /// 레코드셋 Reader Enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<object> GetEnumerator()
        {
            Object[] values = new Object[_reader.FieldCount];

            while (_reader.Read())
            {
                _reader.GetValues(values);

                yield return values;
            }
        }

        /// <summary>
        /// 테이블 존재 여부 확인
        /// </summary>
        /// <param name="tableName">테이블명</param>
        /// <returns>테이블이 있으면 true</returns>
        public bool ExistsTable(string tableName)
        {
            try
            {
                string sql = string.Format("SELECT name FROM sqlite_master WHERE type = 'table' AND name = '{0}'", tableName);

                return ExecuteReader(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 테이블 생성 (이미 존재하면 무시)
        /// </summary>
        /// <param name="tableName">테이블명</param>
        /// <param name="fields">필드 스키마</param>
        /// <returns>테이블을 생성했으면 true</returns>
        public bool CreateTable(string tableName, string fields)
        {
            try
            {
                if (!ExistsTable(tableName))
                {
                    string sql = string.Format("CREATE TABLE {0} ({1})", tableName, fields);

                    ExecuteNonQuery(sql);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 테이블 생성 (이미 존재하면 무시)
        /// </summary>
        /// <param name="tableName">테이블명</param>
        /// <param name="fields">필드 스키마 배열</param>
        /// <returns>테이블을 생성했으면 true</rdasdfeturns>
        public bool CreateTable(string tableName, string[] fields)
        {
            return CreateTable(tableName, string.Join(",", fields));
        }

        /// <summary>
        /// 인덱스 생성asd
        /// </summary>
        /// <param name="indexName">인덱스명</param>
        /// <param name="tableName">테이블명</param>
        /// <param name="fields">인덱스 대상 테이블</param>
        /// <param name="isDrop">인덱스 삭제 후 생성 여부</param>
        public void CreateIndex(string indexName, string tableName, string fields, bool isDrop = false)
        {
            try
            {
                if (isDrop)
                {
                    ExecuteNonQuery("DROP INDEX IF EXISTS " + indexName);
                }

                string sql = string.Format("CREATE INDEX {0} ON {1} ({2})", indexName, tableName, fields);

                ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 인덱스 생성
        /// </summary>
        /// <param name="indexName">인덱스명</param>
        /// <param name="tableName">테이블명</param>
        /// <param name="fields">인덱스 대상 테이블 배열</param>
        /// <param name="isDrop">인덱스 삭제 후 생성 여부</param>
        public void CreateIndex(string indexName, string tableName, string[] fields, bool isDrop = false)
        {
            CreateIndex(indexName, tableName, string.Join(",", fields), isDrop);
        }

        /// <summary>
        /// 마지막 insert id 가져오기
        /// </summary>
        /// <returns>id</returns>
        public int GetLastId()
        {
            try
            {
                string sql = "SELECT last_insert_rowid()";

                if (ExecuteReader(sql))
                {
                    if (_reader.Read())
                    {
                        return Convert.ToInt32(_reader[0]);
                    }
                }

                return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteTable(string tableName)
        {
            try
            {
                if (ExistsTable(tableName))
                {
                    string sql = string.Format("DROP TABLE {0} ", tableName);

                    ExecuteNonQuery(sql);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// DB에서 가장 최근 업데이트된 메이저 버전 정보를 가져오는 함수
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {
            try
            {
                string sql = "SELECT productversion FROM updateinfo WHERE productname = 'BA-Assist' ORDER BY created_at DESC LIMIT 1";

                if (ExecuteReader(sql))
                {
                    if (_reader.Read())
                    {
                        return _reader[0].ToString();
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// DB에서 가장 최근에 업데이트된 디테일 버전 정보를 가져오는 함수
        /// </summary>
        /// <returns></returns>
        public string GetDetailVersion()
        {
            try
            {
                string sql = "SELECT detailversion FROM updateinfo WHERE productname = 'BA-Assist' ORDER BY created_at DESC LIMIT 1";

                if (ExecuteReader(sql))
                {
                    if (_reader.Read())
                    {
                        return _reader[0].ToString();
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
