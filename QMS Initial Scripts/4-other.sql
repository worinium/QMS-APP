ALTER TABLE administrative_source_type
   ALTER COLUMN dms_template_id SET NOT NULL;

ALTER TABLE public.appuser
  ADD COLUMN region_code character varying(20);

ALTER TABLE public.appuser
  ADD FOREIGN KEY (region_code) REFERENCES qms.qms_region (mr_code) ON UPDATE NO ACTION ON DELETE NO ACTION;

Insert into access_rules values('seat_mng', true, 'Login and Manage QMS Seat');
Insert into access_rules values('multiple_seat', true, 'Hold multiple QMS Seats');
