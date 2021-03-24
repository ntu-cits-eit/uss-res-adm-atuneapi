CREATE OR REPLACE TRIGGER SF_STAFF_ATUNE_A_TRIG
  AFTER UPDATE OR DELETE ON SF_STAFF_ATUNE
  FOR EACH ROW
DECLARE
  pACTION CHAR(1);

BEGIN
  IF UPDATING THEN
    pACTION := 'U';
  END IF;
  IF DELETING THEN
    pACTION := 'D';
  END IF;

  insert into SF_STAFF_ATUNE_A
    (st_id_nmbr,
     st_name,
     st_network_acc,
     st_email,
     cdbtitle_desc,
     cdbdept_desc,
     st_business_title,
     added_date,
     action,
     action_date)
  values
    (:OLD.ST_ID_NMBR,
     :OLD.ST_NAME,
     :OLD.ST_NETWORK_ACC,
     :OLD.ST_EMAIL,
     :OLD.CDBTITLE_DESC,
     :OLD.CDBDEPT_DESC,
     :OLD.ST_BUSINESS_TITLE,
     :OLD.ADDED_DATE,
     pACTION,
     SYSDATE);
end SF_STAFF_ATUNE_A_TRIG;
