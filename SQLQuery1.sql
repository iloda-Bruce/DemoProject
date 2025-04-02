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

Create table tblDisease(
	DiseaseID int identity(1,1) primary key,
	DiseaseName nvarchar(50) null
)
Create table tblPhysicalExamination(
	PEID int identity(1,1) primary key,
	PEName nvarchar(50) null
)

Create table tblParaclinicalInvestigation(
	PIID int identity(1,1) primary key,
	PIName nvarchar(50) null
)
Create table tblDiagnosticTests(
	DTID int identity(1,1) primary key,
	PEID int null,
	DTName nvarchar(50) null
)




Create table tblMedicalRecord(
	MedicalRecordID int identity(1,1) primary key,
	VictimID int null,
	DoctorID int null,
	CreateDate Date null,
	MedicalHistory int null,
	Disease int null,
	isComplete bit null,
	Notes nvarchar(200) null
)

Create table MedicalRecordDetail(
	MedicalRecordDetailID int identity(1,1) primary key,
	MedicalRecordID int null,
	VisitDate Date null,
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


CREATE TABLE TaiKhoan (
    MaTaiKhoan INT PRIMARY KEY IDENTITY(1,1),
    TenDangNhap VARCHAR(255) UNIQUE,
    MatKhau VARCHAR(255),
    VaiTro VARCHAR(50), -- 'BenhNhan' hoặc 'BacSi'
    MaNguoiDung INT, -- Liên kết đến MaBenhNhan hoặc MaBacSi
    FOREIGN KEY (MaNguoiDung) REFERENCES BenhNhan(MaBenhNhan),
    FOREIGN KEY (MaNguoiDung) REFERENCES BacSi(MaBacSi)
);

-- Bảng Bệnh nhân
CREATE TABLE BenhNhan (
    MaBenhNhan INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(255),
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    DiaChi NVARCHAR(255),
    SoDienThoai VARCHAR(20),
    Email VARCHAR(255)
);

-- Bảng Bác sĩ
CREATE TABLE BacSi (
    MaBacSi INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(255),
    ChuyenKhoa NVARCHAR(255),
    SoDienThoai VARCHAR(20),
    Email VARCHAR(255)
);

-- Bảng Lịch hẹn
CREATE TABLE LichHen (
    MaLichHen INT PRIMARY KEY IDENTITY(1,1),
    MaBenhNhan INT FOREIGN KEY REFERENCES BenhNhan(MaBenhNhan),
    MaBacSi INT FOREIGN KEY REFERENCES BacSi(MaBacSi),
    NgayHen DATE,
    GioHen TIME,
    TrangThai NVARCHAR(50)
);

-- Bảng Hồ sơ bệnh án
CREATE TABLE HoSoBenhAn (
    MaHoSo INT PRIMARY KEY IDENTITY(1,1),
    MaBenhNhan INT FOREIGN KEY REFERENCES BenhNhan(MaBenhNhan),
    MaBacSi INT FOREIGN KEY REFERENCES BacSi(MaBacSi),
    NgayKham DATE,
    TrieuChung NVARCHAR(MAX),
    ChanDoan NVARCHAR(MAX),
    DonThuoc NVARCHAR(MAX)
);

-- Bảng Thuốc
CREATE TABLE Thuoc (
    MaThuoc INT PRIMARY KEY IDENTITY(1,1),
    TenThuoc NVARCHAR(255),
    DonViTinh NVARCHAR(50),
    DonGia DECIMAL(10, 2),
    SoLuongTon INT
);

-- Bảng Chi tiết đơn thuốc
CREATE TABLE ChiTietDonThuoc (
    MaHoSo INT FOREIGN KEY REFERENCES HoSoBenhAn(MaHoSo),
    MaThuoc INT FOREIGN KEY REFERENCES Thuoc(MaThuoc),
    SoLuong INT,
    PRIMARY KEY (MaHoSo, MaThuoc)
);

-- Bảng Thanh toán
CREATE TABLE ThanhToan (
    MaThanhToan INT PRIMARY KEY IDENTITY(1,1),
    MaHoSo INT FOREIGN KEY REFERENCES HoSoBenhAn(MaHoSo),
    NgayThanhToan DATE,
    TongTien DECIMAL(10, 2),
    HinhThucThanhToan NVARCHAR(50)
);