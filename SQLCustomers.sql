select * from Customer
SET IDENTITY_INSERT Customer ON
declare @Id int
declare @FirstName nvarchar
declare @LastName nvarchar
declare @City nvarchar
declare @Country nvarchar
declare @Phone nvarchar
select @Id = 92
select @FirstName = 'Frank'
select @LastName = 'Cena'
select @City = 'USA'
while @Id >=92 and @Id <= 10000
begin
    insert into Customer(Id, FirstName, LastName, City, Country, Phone) values(@Id, @FirstName + convert(nvarchar(8), @Id), @LastName, @City, @Country, @Phone)
    select @Id = @Id + 1
end