-- Create table
create table SF_STAFF_ATUNE
(
  st_id_nmbr        VARCHAR2(500),
  st_name           VARCHAR2(500),
  st_network_acc    VARCHAR2(500),
  st_email          VARCHAR2(500),
  cdbtitle_desc     VARCHAR2(500),
  cdbdept_desc      VARCHAR2(500),
  st_business_title VARCHAR2(500),
  added_date        DATE default sysdate
);
create index SF_STAFF_ATUNE_IDX1 on SF_STAFF_ATUNE (ST_NETWORK_ACC);
create index SF_STAFF_ATUNE_IDX2 on SF_STAFF_ATUNE (ST_ID_NMBR);
create index SF_STAFF_ATUNE_IDX3 on SF_STAFF_ATUNE (ADDED_DATE);

-- Add comments to the columns 
comment on column SF_STAFF_ATUNE.cdbtitle_desc
  is 'saluation - Dr, Mr, Ms, Professor, etc';
  
 
  
  -- Create table
create table SF_STAFF_ATUNE_TRANS
(
  st_id_nmbr        VARCHAR2(500),
  st_name           VARCHAR2(500),
  st_network_acc    VARCHAR2(500),
  st_email          VARCHAR2(500),
  cdbtitle_desc     VARCHAR2(500),
  cdbdept_desc      VARCHAR2(500),
  st_business_title VARCHAR2(500),
  active            VARCHAR2(1),
  trans_date        DATE default SYSDATE,
  ranking           NUMBER
);
-- Add comments to the columns 
comment on column SF_STAFF_ATUNE_TRANS.ranking
  is '1= records to be deleted; 2 = records to be added';
  
create index SF_STAFF_ATUNE_TRANS_IDX1 on SF_STAFF_ATUNE_TRANS (ST_NETWORK_ACC);
create index SF_STAFF_ATUNE_TRANS_IDX2 on SF_STAFF_ATUNE_TRANS (TRANS_DATE);
create index SF_STAFF_ATUNE_TRANS_IDX3 on SF_STAFF_ATUNE_TRANS (RANKING);

-- Create table
create table SF_STAFF_ATUNE_A
(
  st_id_nmbr        VARCHAR2(500),
  st_name           VARCHAR2(500),
  st_network_acc    VARCHAR2(500),
  st_email          VARCHAR2(500),
  cdbtitle_desc     VARCHAR2(500),
  cdbdept_desc      VARCHAR2(500),
  st_business_title VARCHAR2(500),
  added_date        DATE,
  action            VARCHAR2(1),
  action_date       DATE default sysdate
);
