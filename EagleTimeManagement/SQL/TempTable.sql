CREATE TABLE tblTempTimecards (
	TempCardID int IDENTITY(1,1) primary key not null,
    EmployeeID int not null,
	TimeID int not null FOREIGN KEY REFERENCES tblTimecards(TimeID),
	TimePeriodID int not null,
	TimeDate datetime not null, 
	ProjectID int FOREIGN KEY REFERENCES tblProjects(ProjectID) not null,
	SpecID float not null,
	HourType int FOREIGN KEY REFERENCES tlkpHourTypes(HourType) not null,
	HourTime decimal(20, 6) not null, 
	OnRoad bit,
	CreatedDate datetime null,
	EditedDate datetime null,
	[Add] bit null,
	[Edit] bit null,
	[Delete] bit null
);


--scaffold-dbcontext "Server=LAPTOP-ITQ00KPA\SQLEXPRESS;Database=Questica;Trusted_Connection=True;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer -t tblEmployee, tblProjects, tblSpec, tblTempTimecards, tblTimecardHeader, tblTimecards, tlkpHourTypes -f