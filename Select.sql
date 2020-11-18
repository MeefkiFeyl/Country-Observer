select 
   cd.[Id] 
  ,cd.[Name]        as [��������]
  ,cd.[Alpha3Code]  as [���]
  ,cd.[Area]        as [�������]
  ,cd.[Population]  as [���������]
  ,cd.[Capital]     as [Id �������]
  ,c.[Name]         as [�������]
  ,cd.[Region]      as [Id �������]
  ,r.[Name]         as [������]
  from CountryObserver.dbo.CountryDbs   as cd
  left join CountryObserver.dbo.Cities  as c  on c.Id = cd.Capital
  left join CountryObserver.dbo.Regions as r  on r.Id = cd.Region
