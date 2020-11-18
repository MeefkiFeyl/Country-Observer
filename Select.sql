select 
   cd.[Id] 
  ,cd.[Name]        as [Название]
  ,cd.[Alpha3Code]  as [Код]
  ,cd.[Area]        as [Прощадь]
  ,cd.[Population]  as [Население]
  ,cd.[Capital]     as [Id столицы]
  ,c.[Name]         as [Столица]
  ,cd.[Region]      as [Id региона]
  ,r.[Name]         as [Регион]
  from CountryObserver.dbo.CountryDbs   as cd
  left join CountryObserver.dbo.Cities  as c  on c.Id = cd.Capital
  left join CountryObserver.dbo.Regions as r  on r.Id = cd.Region
