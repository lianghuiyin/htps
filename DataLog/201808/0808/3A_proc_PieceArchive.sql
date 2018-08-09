﻿set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go






create PROCEDURE [dbo].[proc_PieceArchive] 
  @Piece int,
  @Creater int,
  @CreatedDate datetime,
  @OutState int output
AS
SET NOCOUNT ON
begin tran
  set @OutState = 0
  update [Piece] set IsArchived = 1,
    Modifier=@Creater,
    ModifiedDate=@CreatedDate
  where Id = @Piece
  if @@error<>0 
  begin
    set @OutState = -1--归档试件失败
    goto err
  end


  goto succ



err:
  rollback tran;
  return 0
succ:
  commit tran
  set @OutState = 1
  return @Piece













