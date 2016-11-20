declare @NCount int = 10
declare @n int = @NCount

----------*************** ----
truncate table Station_Track
delete from  Station
delete from Track


----------***************------
-- —танции
while @n > 0
begin
	insert into Station(name)
	select 'St1' + cast(@n as varchar)

	set @n = @n -1

end


-- раздадим рандомно веса пут€м
set @n = @NCount  ;

while @n > 0
begin
	insert into Track([weight])
	SELECT ROUND((@NCount - @NCount * RAND()), 0)

	set @n = @n -1
end

-- раздадим рандом пути станци€м (ребра вершинам графа)
set @n = @NCount * @NCount/2

declare @smin int,  @tmin int
select @smin = min(id) from Station
select @tmin = min(id) from Track

declare @ids_start int, @ids_stop int, @idt int
while @n > 0
begin

	select @ids_start =  @smin + ROUND((@NCount - @NCount * RAND()), 0) - 1
	select @ids_stop =  @smin + ROUND((@NCount - @NCount * RAND()), 0) - 1
	select @idt = @tmin + ROUND((@NCount - @NCount * RAND()), 0) - 1

	if not exists(select * from Station_Track 
	where idstation_start = @ids_start and idstation_stop = @ids_stop and idtrack = @idt) 
		and exists (select id from Station where id = @ids_start) 
		and exists (select id from Station where id = @ids_stop)
		and exists (select id from Track where id = @idt
		and @ids_start <> @ids_stop)
	begin
		insert into Station_Track(idstation_start, idstation_stop, idtrack)
		select @ids_start, @ids_stop, @idt 

		set @n = @n - 1
	end
end

select * from Station_Track
select * from Track
select * from Station