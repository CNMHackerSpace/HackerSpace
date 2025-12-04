# 🐳 SQL Server on macOS (Dockerized) — Developer Setup Guide

This guide explains how to run **Microsoft SQL Server** locally on a Mac using **Docker**, so development is consistent across Windows, Linux, and macOS environments.

SQL Server does **not** run natively on macOS, but Microsoft provides an official **SQL Server for Linux** image that works perfectly through Docker.

---

## 📦 Prerequisites

### 1. Install Docker Desktop for macOS  
Download and install:

https://www.docker.com/products/docker-desktop/

After installation:

- Start Docker Desktop  
- Verify that Docker says **“Running”** in the macOS menu bar  

---

## 🏗 Step 1 — Pull & Run SQL Server Container

Open a terminal and run:

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong!Passw0rd"    -p 1433:1433 --name sqlserver    -d mcr.microsoft.com/mssql/server:2022-latest
```

### What this does:

- Starts SQL Server 2022 in a Docker container  
- Accepts the MS license (`ACCEPT_EULA=Y`)  
- Sets the SA admin password (`SA_PASSWORD`)  
- Exposes SQL Server on port **1433**  
- Runs in detached mode (`-d`)  
- Names the container `sqlserver`  

**Password rules:**  
SQL Server requires a strong password:
- At least 8 characters  
- Contains uppercase, lowercase, digit, and symbol  

---

## 🔍 Step 2 — Verify SQL Server Is Running

Check running containers:

```bash
docker ps
```

Check logs:

```bash
docker logs sqlserver
```

You should see:

```
SQL Server is now ready for client connections.
```

---

## 🧪 Step 3 — Connect Using Azure Data Studio

Install Azure Data Studio:

https://learn.microsoft.com/sql/azure-data-studio/download

Use these settings:

| Field | Value |
|------|--------|
| **Server** | `localhost,1433` |
| **Authentication** | SQL Login |
| **Username** | `sa` |
| **Password** | `YourStrong!Passw0rd` |
| **Trust server certificate** | Yes |

Create a database:

```sql
CREATE DATABASE HackerSpace;
```

---

## 🛠 Step 4 — Update `appsettings.json` for Local Development

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=HackerSpace;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;"
  }
}
```

Update `Program.cs`:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
```

Run EF Core migrations:

```bash
dotnet ef database update
```

---

## 🔁 Common Docker Commands

```bash
docker stop sqlserver
docker start sqlserver
docker restart sqlserver
docker rm -f sqlserver
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

---

## 🧯 Troubleshooting

### Login failed for user 'sa'
Use a stronger password.

### Port 1433 is already in use
Map a different port:

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong!Passw0rd"    -p 11433:1433 --name sqlserver    -d mcr.microsoft.com/mssql/server:2022-latest
```

Connect with:

```
localhost,11433
```

### SQL Server never becomes ready
Increase Docker RAM to at least **4GB**.

---

## 🧭 Optional: Persist Data Between Container Restarts

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong!Passw0rd"    -p 1433:1433 --name sqlserver    -v sql_data:/var/opt/mssql    -d mcr.microsoft.com/mssql/server:2022-latest
```

---

## 🎉 You're Ready to Develop on macOS!

Mac developers can now:

- Run SQL Server locally  
- Apply EF Core migrations  
- Use Azure Data Studio for DB management  
- Work consistently with Windows/Linux developers  
