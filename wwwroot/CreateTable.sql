-- create table for database
use webtools;

drop table if exists User_Table;

create table User_Table (
	wechatID varchar(50) primary key
);

drop table if exists Pet_Table;

create table Pet_Table (
	petID int auto_increment not null primary key,
    wechatID_Fkey int not null,
    petName varchar(50) not null,
    petWeight int unsigned not null,
    petHungry int unsigned not null,
    petAge int unsigned not null default 0,
    returned bool default false
);

insert into User_Table values ("123");
insert into User_Table values ("234");

insert into Pet_Table values (1,"123","testcat1",1,1,1,true);

insert into Pet_Table values (2,"234","testcat2",1,1,1,false);

insert into Pet_Table values (3,"123","testcat3",1,1,1,false);