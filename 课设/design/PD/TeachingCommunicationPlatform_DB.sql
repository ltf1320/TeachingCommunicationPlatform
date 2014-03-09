/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     2014/3/9 12:12:06                            */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('manageCou') and o.name = 'FK_MANAGECO_REFERENCE_USER')
alter table manageCou
   drop constraint FK_MANAGECO_REFERENCE_USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('manageCou') and o.name = 'FK_MANAGECO_REFERENCE_COURSE')
alter table manageCou
   drop constraint FK_MANAGECO_REFERENCE_COURSE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('"user"') and o.name = 'FK_USER_REFERENCE_ROLE')
alter table "user"
   drop constraint FK_USER_REFERENCE_ROLE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Course')
            and   type = 'U')
   drop table Course
go

if exists (select 1
            from  sysobjects
           where  id = object_id('manageCou')
            and   type = 'U')
   drop table manageCou
go

if exists (select 1
            from  sysobjects
           where  id = object_id('role')
            and   type = 'U')
   drop table role
go

if exists (select 1
            from  sysobjects
           where  id = object_id('"user"')
            and   type = 'U')
   drop table "user"
go

/*==============================================================*/
/* Table: Course                                                */
/*==============================================================*/
create table Course (
   couId                character varying(256) not null,
   couName              char varying(256)    null,
   type                 char varying(10)     null,
   stuNum               int                  null,
   term                 character(5)         null,
   constraint PK_COURSE primary key (couId)
)
go

/*==============================================================*/
/* Table: manageCou                                             */
/*==============================================================*/
create table manageCou (
   userName             character varying(256) not null,
   couId                character varying(256) not null,
   constraint PK_MANAGECOU primary key (userName, couId)
)
go

/*==============================================================*/
/* Table: role                                                  */
/*==============================================================*/
create table role (
   roleId               int                  not null,
   roleName             character varying(256) null,
   comment              character varying(256) null,
   constraint PK_ROLE primary key (roleId)
)
go

/*==============================================================*/
/* Table: "user"                                                */
/*==============================================================*/
create table "user" (
   userName             character varying(256) not null,
   roleId               int                  not null,
   nickName             character varying(256) null,
   pwd                  character varying(256) not null,
   email                character varying(256) null,
   createDate           datetime             null,
   isLocked             bit                  null,
   constraint PK_USER primary key (userName)
)
go

alter table manageCou
   add constraint FK_MANAGECO_REFERENCE_USER foreign key (userName)
      references "user" (userName)
go

alter table manageCou
   add constraint FK_MANAGECO_REFERENCE_COURSE foreign key (couId)
      references Course (couId)
go

alter table "user"
   add constraint FK_USER_REFERENCE_ROLE foreign key (roleId)
      references role (roleId)
go

