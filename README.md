# DBInvador
DB Invador allows us to view selective records of a table with ease. It helps us to explore all available databases in SQL Server(we can provide multiple DB Servers/Instances simultaneously), tables under them and data contained in tables. We can select any database and browse their tables. Select any table and look into data contained in them. We can open multiple instances of a server and compare their data side by side.

- Configure multiple servers
- Open multiple instances of same server
- Quick browsing of data
- Specify number of records we want to view at per instance level
- Filter tables

##Installing Application
Download the application from Github and run setup from Installation folder. It will create a shortcut menu in `Start` -> `techPhernalia`

- [Latest build](https://codeload.github.com/techphernalia/DBInvador/zip/master)
- [Stable Version 2.0.0.7](https://github.com/techphernalia/DBInvador/archive/Version_2_0_0_7.zip)

##Configure New Server
Configure new server allows us to provide a friendly name and connection string for the SQL server which can be used later to invade database. In order to configure a new server, follow below steps

![Configure New Server](https://raw.githubusercontent.com/techphernalia/DBInvador/master/Images/Create%20New%20Server.png)
 

- Navigate to **Servers** -> **New Server** (`Ctrl + N`) to open New Server configuration wizard.
- Select SQL Server type
	- MssqlServer for Microsoft SQL Server
- Give it a user friendly name
- Provide connectionstring for the MS SQL Server as applicable
- Click on **Save** (`Alt + S`) and configuration will be saved.

**PS**

- All configured servers can be seen in Servers menu and individual server type menu.
- Configuring a new server does not check for the existence and/or correctness of connection string provided.
- Configuring new server do not create/install any server

##Remove a configuration
Navigate to a configuration from **Servers** menu or individual server type(e.g. *MssqlServer*) menu and click **Delete** item next to it.

![Delete a Server](https://raw.githubusercontent.com/techphernalia/DBInvador/master/Images/Delete%20a%20Configuration.png)

**PS**

- Deleting a configuration only removes item from the menu
- It does not delete/uninstalls server

## Browsing a server
![Browse a Table](https://raw.githubusercontent.com/techphernalia/DBInvador/master/Images/Browse%20a%20table.png)
###Connect a server
Connecting a server allows us to view data of the server. In order to connect to a server navigate to **Servers** and click on the server we want to connect to. We can also chose Server from individual server type menu or click on Connect item next to a server.
###Select a database
Once we are connected to a server, it displays list of databases available in the server. Select one from the drop down to use the database.

- If new database is created while server is connected, same will not be reflected in drop down. Close the window(DB Invader server instance) and connect to server again to view changes.

###Viewing data from a table
Once we have selected a database, it displays tables in the database in another drop down. Selecting a table shows top 10 records from the table.

- If new table is created while a database is selected, same is note reflected on UI. Change database and tables list will be reflected.

**PS**

- Selecting a table shows records in read only view.
- No changes are saved in the server and no modifications are made to server/database/table.
- Any changes made in underlying table is not reflected on UI. 

##Filtering tables
By default drop down for table shows all the tables available in the database. Typing something in the textbox filters list of tables. Table are shown in drop down in `[schema_name].[table_name]` format. Filter works as like SQL clause. e.g.

- `abc` will only show tables which contains abc in schema or table
- `a.bc` will only show tables which contains a.bc in schema or table
- `[abc` will only show tables where table or schema name starts with abc
- `abc]` will only show tables where table or schema name ends with abc
- `.[abc` will only show tables where table name starts with abc
- `abc].` will only show tables where schema name ends with abc
- `a].[bc` will only show tables where schema name ends with a and table name starts with bc

**PS**

- Filter text change does all the filtering at application level and no SQL bandwidth is used for same.

##Number of records to fetch
We can specify number of records to fetch in textbox right after table drop down.

- If text box is blank, `10` records will be fetched.
- If text box value is `0` only schema of table will be visible.
- In order to view all the records of table we need to provide a large value in textbox.

##Connection String

- Microsoft SQL Server
	- Windows Authentication: `Data Source=server_instance;Integrated Security=True;`
	- SQL Authentication : `Data Source=server_instance;User Id=user_name;Password=password;`

---

techPhernalia : [Website](https://www.techphernalia.com) | [Facebook](https://www.facebook.com/techphernalia) | [Twitter](https://www.twitter.com/techphernalia) 

Durgesh : [Website](https://www.durgesh.org) | [Facebook](https://www.facebook.com/dcbhai) | [Twitter](https://www.twitter.com/durgeshjee)

---