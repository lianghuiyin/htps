set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go









ALTER PROCEDURE [dbo].[proc_StartupSelect] 
AS
begin
  select * from [User]
  select * from [Role]
  select * from [Power]
  select * from [Project]
  select * from [Department]
  select * from [Oil]
  select * from [Preference]
  select * from [Car] where IsArchived = 0
  select * from [Instance] where IsArchived = 0
  select * from [Trace] where IsArchived = 0
  select top 20 * from [Bill] order by Id desc
  select * from [Piece] where IsArchived = 0
  --select top 5 * from [Signature] order by Id desc
end




set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON





