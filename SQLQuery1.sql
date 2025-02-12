Create database ClinicManagement 

Create table tblDoctors(
	DoctorID int identity(1,1) primary key 
	, DoctorName nvarchar(50) null
	, PhoneNumber nvarchar(50) null
	, DOB Date null
	, DoctorAddress nvarchar(100) null 
	, AccountID int null
)

Create table tblVictims(
	VictimID int identity(1,1) primary key 
	, VictimName nvarchar(50) null
	, PhoneNumber nvarchar(50) null
	, DOB Date null
	, VictimAddress nvarchar(100) null 
	, AccountID int null
)

Create table tblAccounts(
	AccountID int identity(1,1) primary key,
	AccountName nvarchar(50) null,
	AccountPass nvarchar(50) null,
	AccountRole bit null
)

Create table tblMedicalRecord(
	MedicalRecordID int identity(1,1) primary key,
	VictimID int null,
	DoctorID int null,
	CreateDate Date null,
	Disease nvarchar(50) null,
	isComplete bit null
)

Create table MedicalRecordDetail(
	MedicalRecordDetailID int identity(1,1) primary key,
	MedicalRecordID int null,
	ClinicID int null,
	LabID int null,
	VisitDate Date null,
	Disease nvarchar(50) null,
	Diagnosis nvarchar(50) null,
	ReportID int null,
	HasResult bit null,
	Amount float null
)

Create table Reports(
	ReportID int identity(1,1) primary key,
	MedicalRecordDetailID int null,
	CreateDate Date null,
	ReportDate Date null,
	Disease nvarchar(50) null,
	DoctorID int null
)

Create table ReportsDetails(
	ReportsDetailID int identity(1,1) primary key,
	ReportID int null,
	ReportDate Date null,
	SpecimenID int null,
	SpecimenName nvarchar(50) null,
	
)
Create table ReportSpecimens(
	ReportsDetailID int null,
	SpecimenID int null,
	SpecimenName nvarchar(50) null,
	UnitID int null,
	Result nvarchar(50) null,
	Diagnosis nvarchar(50) null,
	StandardResult nvarchar(50) null
)