
if not exists(select * from syscolumns where id=object_id('Piece') and name='IsArchived')
	alter table [Piece] add [IsArchived] bit not null default(0)


declare @constraint_name varchar(200)
select @constraint_name=b.name from syscolumns a,sysobjects b where a.id=object_id('Piece') and b.id=a.cdefault and a.name='IsEnable' and b.name like 'DF%'
select @constraint_name
if @constraint_name is not null
  exec('alter table '+ '[Piece]' +' drop constraint '+@constraint_name)
if exists(select * from syscolumns where id=object_id('Piece') and name='IsEnable')
  alter table [Piece] drop column [IsEnable]

Go
