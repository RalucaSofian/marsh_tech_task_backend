FROM mcr.microsoft.com/mssql/server:2025-latest

USER root

RUN apt-get update
RUN apt-get install -yq gnupg gnupg2 gnupg1 curl apt-transport-https

RUN curl https://packages.microsoft.com/keys/microsoft.asc -o /var/opt/mssql/ms-key.cer
RUN apt-key add /var/opt/mssql/ms-key.cer
RUN curl https://packages.microsoft.com/config/ubuntu/24.04/mssql-server-2025.list -o /etc/apt/sources.list.d/mssql-server-2025.list
RUN apt-get update

# Install Full Text Search on top of MS SQL Server 2025
RUN apt-get install -y mssql-server-fts
