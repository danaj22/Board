﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Board.Migrations
{
    /// <inheritdoc />
    public partial class AddViewTopAuthors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW View_TopAuthors AS SELECT top(5) u.FullName, count(*) as [WorkItemsCreated]
                                  FROM [BoardsDb].[dbo].[WorkItems] wi
                                  JOIN Users u on u.Id = wi.AuthorId
                                  group by u.Id, u.FullName order by [WorkItemsCreated] desc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW View_TopAuthors");
        }
    }
}
