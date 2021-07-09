CREATE DATABASE if not exists emetro;

use emetro;

CREATE TABLE CongTy 
(
	maCongTy nvarchar(20) primary key not null,
    tenCongTy varchar(45) not null,
    diaChiWeb varchar(45) not null,
    diaChiTruSo varchar(100) not null,
    sdt varchar(15) not null
);
CREATE TABLE TrangThaiGaTau
(
	maTTGaTau nvarchar(20) primary key not null,
    trangThai varchar(45) not null
);

-- data 
insert into TrangThaiGaTau values ('binhThuong', N'Bình thường');
insert into TrangThaiGaTau values ('dangSua', N'Đang sửa chữa');
insert into TrangThaiGaTau values ('ngungHD', N'Ngừng hoạt động');

CREATE TABLE GaTau 
(
	maGaTau nvarchar(20) primary key not null,
    tenGaTau varchar(45) not null,
    moTaViTri varchar(45) not null,
    trangThai nvarchar(20) not null,
    anhGaTau varchar(60),
    
    foreign key (trangThai) references TrangThaiGaTau(maTTGaTau)
);

insert into GaTau values ('ga001', N'Suối tiên', N'Q. Thủ Đức', 'binhThuong', 'iphoneXas');
insert into GaTau values ('ga002', N'ĐHQG', N'Q. Thủ Đức', 'binhThuong', 'iphoneXas');
insert into GaTau values ('ga003', N'Bình Thái', N'Q. Thủ Đức', 'binhThuong', 'iphoneXas');
insert into GaTau values ('ga004', N'An Lạc', N'Q. Thủ Đức', 'binhThuong', 'iphoneXas');

CREATE TABLE TinhTrangTuyenTau
(
	maTTTuyenTau nvarchar(20) primary key not null,
    tinhTrang varchar(45)
);
-- data
insert into TinhTrangTuyenTau values ('hoatDong', N'Hoạt động');
insert into TinhTrangTuyenTau values ('kHoatDong', N'Không hoạt động');

CREATE TABLE TuyenTau
(
	maTuyen nvarchar(20) primary key not null,
    tenTuyen varchar(45) not null,
    maCongTy nvarchar(20) not null,
    gaDau nvarchar(20) not null,
    gaCuoi nvarchar(20) not null,
    loaiTuyen varchar(10) not null,
    gioBatDau time not null,
    gioKetThuc time not null,
    thoiGianChoTB integer,
    giaVe decimal(18, 0),
    tinhTrang nvarchar(10),
    
    foreign key (maCongTy) references CongTy(maCongTy),
	foreign key (gaDau) references GaTau(maGaTau),
    foreign key (gaCuoi) references GaTau(maGaTau),
    foreign key (tinhTrang) references TinhTrangTuyenTau(maTTTuyenTau)
);

CREATE TABLE VeTau
(
	maVe int primary key not null  auto_increment,
    loaiVe 	nvarchar(20) not null,
    maTuyen nvarchar(20) not null,
    giaVe decimal(16, 0) not null,
    ngayMua date,
    tinhTrang varchar(10) not null,
    
    foreign key (maTuyen) references TuyenTau(maTuyen)
);

CREATE TABLE UserRole
(
	userRoleId nvarchar(20) primary key not null,
    userRole nvarchar(50) not null
);
-- data
insert into UserRole values ('1', N'Nhân viên Sở Giao thông thành phố');
insert into UserRole values ('2d1', N'Nhân viên Công ty Quản lý tuyến số 1');

CREATE TABLE User
(
	userId int primary key not null auto_increment,
    userName nvarchar(30) not null,
    userRoleId nvarchar(10) not null,
    
    foreign key (userRoleId) references UserRole(userRoleId)
);
-- Command!!!!!!!!!!
select * from TrangThaiGaTau;