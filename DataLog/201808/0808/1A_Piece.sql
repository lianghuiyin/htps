
if not exists(select * from syscolumns where id=object_id('Piece') and name='IsArchived')
	alter table [Piece] add [IsArchived] bit not null default(0)



