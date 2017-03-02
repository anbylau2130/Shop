   delete from SysPrivilege where ID<>0;
   delete from SysRolePrivilege where ID<>0;
   delete from SysMenu where ID<>0;
   delete from SysRoleMenu where ID<>0;

insert into SysRolePrivilege  (Privilege,Role,Creator,createTime,Auditor,AuditTime)
select ID as Privilege,
0 as Role,
0 as Creator,
GETDATE() as createTime,
0 as Auditor,
GETDATE() as AuditTime
from SysPrivilege as a
where  a.ID<>0;

insert into SysRoleMenu  (Menu,Role,Creator,createTime,Auditor,AuditTime)
select ID as Menu,
0 as Role,
0 as Creator,
GETDATE() as createTime,
0 as Auditor,
GETDATE() as AuditTime
from SysMenu as a
where  a.ID<>0;