namespace GameStore.DAL.Infrastructure.Utils
{
    public static class DbIdentifier
    {
        public static short DbCoefficient => 10;

        public static int EncodeId(int id, DbType db)
        {
            return id * DbCoefficient + (int)db;
        }

        public static int EncodeId(int id, int dbId)
        {
            return id * DbCoefficient + dbId;
        }

        public static int DecodeId(int id)
        {
            return id / DbCoefficient;
        }

        public static DbType DecodeDbId(int id)
        {
            return (DbType)(id % DbCoefficient);
        }
    }

    public enum DbType
    {
        GameStore = 1,

        Northwind = 2
    }
}