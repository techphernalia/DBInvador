# DBInvador
Easy way to peek into SQL Servers. It helps you explore all available databases in SQL Server(you can provide multiple DB Servers/Instances simultaneously), tables under them and data contained in tables. You can select any database and browse their tables. Select any table and look into data contained in them.

##Configure New Server##
Configure new server allows you to provide a friendly name and connection string for the SQL server which can be used later to invade database. In order to configure a new server, follow below steps

![Configure New Server](://www.techphernalia.com/wp-content/uploads/2016/04/Create-New-Server.png) 

- Navigate to **Servers** -> **New Server** (`Ctrl + N`) to open New Server configuration wizard.
- Select SQL Server type
	- MssqlServer for Microsoft SQL Server
- Give it a user friendly name
- Provide connectionstring for the MS SQL Server as applicable
- Click on **Save** (`Alt + S`) and configuration will be saved.

PS

- All configured servers can be seen in Servers menu and individual server type menu.
- Configuring a new server does not check for the existence and/or correctness of connection string provided.
