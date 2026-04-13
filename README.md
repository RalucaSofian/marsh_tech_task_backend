# DeviceManagement

.NET backend for the MarshTech recruitment task.

## Running the project locally

To set up the database, run:

```bash
> cd Infra/
> docker compose up -d
```

After the docker image is up, we can set up the database itself.
We can do that either through EF Core, usingL

```bash
> cd ..
> cd Migrations/
> dotnet ef database update
```

Or we can run the SQL scripts directly:

```bash
> cd ..
> cd SQL/
> sqlcmd -d DeviceManagementDb -U sa -P strongPwdFor@Server1 -S 127.0.0.1,1433 -i create_DB_and_Tables.sql
> sqlcmd -d DeviceManagementDb -U sa -P strongPwdFor@Server1 -S 127.0.0.1,1433 -i populate_tables.sql
```

This step is mandatory for the Full Text Search to be functioning.

Next, we can run the backend:

```bash
> dotnet watch
```

## Requirements

- .NET 9.0
- docker
