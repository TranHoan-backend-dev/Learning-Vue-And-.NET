delimiter //
create procedure Proc_Employee_Insert(
    in EmployeeCode nvarchar(36),
    in FullName nvarchar(255),
    in Username nvarchar(255),
    in Gender nvarchar(255),
    in Email nvarchar(50),
    in Age int,
    in Address nvarchar(255),
    in DepartmentId nvarchar(36)
)
begin
    insert into employees(EmployeeId, EmployeeCode, Username, Email, Fullname, Age, Address, DepartmentId)
    values (uuid(), EmployeeCode, Username, Email, FullName, Age, Address, DepartmentId);
end //
delimiter ;

# Kiem tra xem nhan vien co bi trung hay khong
delimiter //
create procedure Proc_Employee_CheckDuplicate(
    
)
begin 
end //
delimiter ;

# Kiem tra xem nhan vien da ton tai hay chua
delimiter //
create procedure Proc_Employee_IsExist(
    
)
begin 
    
end //
delimiter ;