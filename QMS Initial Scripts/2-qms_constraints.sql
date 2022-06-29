----
ALTER TABLE qms.qms_service
ADD FOREIGN KEY (region_code) 
REFERENCES qms.qms_region (mr_code)
                       ON DELETE NO ACTION
                       ON UPDATE NO ACTION,
ADD FOREIGN KEY (service_type) 
REFERENCES qms.qms_service_type (mr_code)
                       ON DELETE NO ACTION
                       ON UPDATE NO ACTION,
ADD UNIQUE (service_type, region_code);
-----
ALTER TABLE qms.qms_seat
ADD FOREIGN KEY (service_id) 
REFERENCES qms.qms_service (service_id)
                       ON DELETE NO ACTION
                       ON UPDATE NO ACTION,
ADD FOREIGN KEY (assigned_id) 
REFERENCES public.appuser (appuser_id)
                       ON DELETE NO ACTION
                       ON UPDATE NO ACTION,
ADD FOREIGN KEY (modified_by) 
REFERENCES public.appuser (appuser_id)
                       ON DELETE NO ACTION
                       ON UPDATE NO ACTION;
-----
ALTER TABLE qms.qms_queue
ADD FOREIGN KEY (service_id) 
REFERENCES qms.qms_service (service_id)
                       ON DELETE NO ACTION
                       ON UPDATE NO ACTION,
ADD FOREIGN KEY (assigned_id) 
REFERENCES public.appuser (appuser_id)
                       ON DELETE NO ACTION
                       ON UPDATE NO ACTION,
ADD FOREIGN KEY (queue_status) 
REFERENCES qms.qms_queue_status (mr_code)
                       ON DELETE NO ACTION
                       ON UPDATE NO ACTION,
ADD FOREIGN KEY (seat_id) 
REFERENCES qms.qms_seat (seat_id)
                       ON DELETE NO ACTION
                       ON UPDATE NO ACTION;