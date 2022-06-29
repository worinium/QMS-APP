insert into qms.qms_service_type values ('helpdesk',true, 'Help Desk');
insert into qms.qms_service_type values ('appsub',true, 'Application Submission');
insert into qms.qms_service_type values ('conveyance',true, 'Conveyance');
insert into qms.qms_service_type values ('legalsearch',true, 'Legal Search');
insert into qms.qms_service_type values ('cashier',true, 'Cashier');
insert into qms.qms_service_type values ('dgoffice',true, 'DG Office');


insert into qms.qms_queue_status values ('complete',true, 'Token Completed');
insert into qms.qms_queue_status values ('waiting',true, 'Token Waiting');
insert into qms.qms_queue_status values ('skipped',true, 'token Skipped');
insert into qms.qms_queue_status values ('noresponse',true, 'No Response');
insert into qms.qms_queue_status values ('processing',true, 'Token Processing');
insert into qms.qms_queue_status values ('kicked',true, 'Kicked from Queue');
insert into qms.qms_queue_status values ('logout',true, 'Logged out of Queue');

insert into qms.qms_setting values ('max_service_seat', '15', true, 'Maximum Number of seats per service');

--Color Codes for Edogis 
insert into qms.qms_setting values ('footer', '#ffffff', true, 'Current Token background Color');
insert into qms.qms_setting values ('tabletAppBackground', '#ffffff', true, 'Tablet App background Color');
insert into qms.qms_setting values ('header', '#ffffff', true, 'Header Menu For Tablet Screen Background Color');
insert into qms.qms_setting values ('WaitingRoomheader', '#ffffff', true, 'Header Menu For Waiting Screen Background Color');
insert into qms.qms_setting values ('buttonsColor', '#FFA500', true, 'Services Button Background Color');
insert into qms.qms_setting values ('gridColor', '#ffffff', true, 'DataGrid Background Color');
insert into qms.qms_setting values ('textBGColor', '#ffffff', true, 'Text Background Color');
insert into qms.qms_setting values ('textFGColor', '#FF1E5F06', true, 'Text Color');
insert into qms.qms_setting values ('gridLineColor', '#FF1E5F06', true, 'Gridline Color');
--End of Edogis

--Color Codes for Kadgis 
insert into qms.qms_setting values ('footer', '#ffffff', true, 'Current Token background Color');
insert into qms.qms_setting values ('tabletAppBackground', '#ffffff', true, 'Tablet App background Color');
insert into qms.qms_setting values ('header', '#ffffff', true, 'Header Menu For Tablet Screen Background Color');
insert into qms.qms_setting values ('WaitingRoomheader', '#ffffff', true, 'Header Menu For Waiting Screen Background Color');
insert into qms.qms_setting values ('buttonsColor', '#ffad2e', true, 'Services Button Background Color');
insert into qms.qms_setting values ('gridColor', '#ffffff', true, 'DataGrid Background Color');
insert into qms.qms_setting values ('textBGColor', '#ffffff', true, 'Text Background Color');
insert into qms.qms_setting values ('textFGColor', '#000066', true, 'Text Color');
insert into qms.qms_setting values ('gridLineColor', '#000066', true, 'Gridline Color');
--End Color Codes for Kadgis 

insert into qms.qms_setting values ('agency', 'Kaduna Geographic Information Service', true, 'Service Center A');
insert into qms.qms_setting values ('datagrid_timer', '10', true, '10s auto refresh');
insert into qms.qms_setting values ('caller_timer', '30', true, '30s auto refresh');