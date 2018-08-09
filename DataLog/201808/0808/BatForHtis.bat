sqlcmd -S localhost -U sa -P MtHorizon2003h -d HTPS -i 1A_Piece.sql
sqlcmd -S localhost -U sa -P MtHorizon2003h -d HTPS -i 2A_proc_StartupSelect.sql
sqlcmd -S localhost -U sa -P MtHorizon2003h -d HTPS -i 3A_proc_PieceArchive.sql
sqlcmd -S localhost -U sa -P MtHorizon2003h -d HTPS -i 4A_proc_PieceRestore.sql
pause

