IF  NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = N'BulletinBoard')
    BEGIN
        CREATE DATABASE [BulletinBoard]
    END;

IF  NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'BulletinBoard')
        BEGIN
            CREATE DATABASE [BulletinBoard]
        END;

IF  NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'BulletinBoard')
            BEGIN
                CREATE DATABASE [BulletinBoard]
        END;