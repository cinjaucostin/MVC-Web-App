select * from Product
SET IDENTITY_INSERT Product ON
declare @Id int
declare @ProductName nvarchar(50)
declare @SupplierId int
declare @UnitPrice decimal(12,2)
declare @Package nvarchar(30)
declare @IsDiscontinued bit
select @Id = 79
select @SupplierId = 23
select @ProductName = 'OldSpice'
select @IsDiscontinued = 0
while @Id >=79 and @Id <= 1000
begin
    insert into Product(Id, ProductName, SupplierId, Package, IsDiscontinued) values(@Id, @ProductName, @SupplierId, @Package, @IsDiscontinued)
    select @Id = @Id + 1
end