create database QLCB;
use QLCB;

create table ChucVu(
	maCV int primary key,
	tenCv nvarchar(20));

drop table ChucVu;
drop table CanBo;
create table CanBo(
	maCB int,
	tenCB nvarchar(30),
	chucVuCB int,
	soGioGiang int,
	donGia money,
	constraint fk_cv foreign key (chucVuCB) references ChucVu(maCV));

INSERT INTO ChucVu (MaCV, TenCV) VALUES (1, N'Giảng viên');

select max(maCB) from CanBo;
select * from CanBo;
