
use HOTEL_MANAGEMENT;
create table room(
	roomID int primary key,
	roomName Nvarchar(20)
)

drop table room;
drop table customer;

CREATE TABLE customer (
    ID INT PRIMARY KEY,
    name NVARCHAR(30),
    CCCD NVARCHAR(15),
    total MONEY,
    checkOutDate DATETIME,
    room INT,
    CONSTRAINT fk_room FOREIGN KEY (room) REFERENCES room(roomID),
    CONSTRAINT chk_total_positive CHECK (total > 0),
    CONSTRAINT chk_cccd_length CHECK (LEN(CCCD) BETWEEN 9 AND 12)
);
select * from room;


CREATE PROCEDURE INSERT_CUSTOMER
    @id INT,
    @name NVARCHAR(30),
    @cccd NVARCHAR(12),
    @total MONEY,
    @time DATETIME,
    @room INT
AS
BEGIN 
    INSERT INTO customer (ID, name, CCCD, total, checkOutDate, room)
    VALUES (@id, @name, @cccd, @total, @time, @room);
END;

EXEC INSERT_CUSTOMER 1,'John Doe','123456789012',100.00,'2024-01-29 12:00:00',1;






Insert into room values('1', 'A101');
Insert into room values('2', 'A102');
Insert into room values('3', 'A103');
Insert into room values('4', 'A104');
Insert into room values('5', 'A105');
Insert into room values('6', 'A201');
Insert into room values('7', 'A202');
Insert into room values('8', 'A203');
Insert into room values('9', 'A204');
Insert into room values('10', 'A205');
Insert into room values('11', 'A301');
Insert into room values('12', 'A302');
Insert into room values('13', 'A303');
Insert into room values('14', 'A304');
Insert into room values('15', 'A305');
Insert into room values('16', 'B101');
Insert into room values('17', 'B102');
Insert into room values('18', 'B103');
Insert into room values('19', 'B104');
Insert into room values('20', 'B105');
Insert into room values('21', 'B201');
Insert into room values('22', 'B202');
Insert into room values('23', 'B203');
Insert into room values('24', 'B204');
Insert into room values('25', 'B205');
Insert into room values('26', 'B301');
Insert into room values('27', 'B302');
Insert into room values('28', 'B303');
Insert into room values('29', 'B304');
Insert into room values('30', 'B305');
