/*
 Navicat Premium Data Transfer

 Source Server         : POSTGRE SQL
 Source Server Type    : PostgreSQL
 Source Server Version : 90619 (90619)
 Source Host           : localhost:5432
 Source Catalog        : projeto.portfolio
 Source Schema         : public

 Target Server Type    : PostgreSQL
 Target Server Version : 90619 (90619)
 File Encoding         : 65001

 Date: 11/06/2024 20:43:54
*/


-- ----------------------------
-- Sequence structure for tb_estoque_saida_produto_ite_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."tb_estoque_saida_produto_ite_id_seq";
CREATE SEQUENCE "public"."tb_estoque_saida_produto_ite_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 9223372036854775807
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for tb_estoque_saida_sai_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."tb_estoque_saida_sai_id_seq";
CREATE SEQUENCE "public"."tb_estoque_saida_sai_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 9223372036854775807
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for tb_produto_estoque_est_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."tb_produto_estoque_est_id_seq";
CREATE SEQUENCE "public"."tb_produto_estoque_est_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 9223372036854775807
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for tb_produto_pro_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."tb_produto_pro_id_seq";
CREATE SEQUENCE "public"."tb_produto_pro_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 9223372036854775807
START 1
CACHE 1;

-- ----------------------------
-- Table structure for tb_estoque_saida
-- ----------------------------
DROP TABLE IF EXISTS "public"."tb_estoque_saida";
CREATE TABLE "public"."tb_estoque_saida" (
  "sai_id" int4 NOT NULL DEFAULT nextval('tb_estoque_saida_sai_id_seq'::regclass),
  "sai_dtreg" timestamp(6) NOT NULL
)
;

-- ----------------------------
-- Records of tb_estoque_saida
-- ----------------------------
INSERT INTO "public"."tb_estoque_saida" VALUES (8, '2024-06-11 19:20:26.271931');
INSERT INTO "public"."tb_estoque_saida" VALUES (9, '2024-06-11 19:48:48.545819');

-- ----------------------------
-- Table structure for tb_estoque_saida_produto
-- ----------------------------
DROP TABLE IF EXISTS "public"."tb_estoque_saida_produto";
CREATE TABLE "public"."tb_estoque_saida_produto" (
  "ite_id" int4 NOT NULL DEFAULT nextval('tb_estoque_saida_produto_ite_id_seq'::regclass),
  "est_id" int4 NOT NULL,
  "sai_id" int4 NOT NULL,
  "est_qtd" int4 NOT NULL,
  "est_preco" money NOT NULL
)
;

-- ----------------------------
-- Records of tb_estoque_saida_produto
-- ----------------------------
INSERT INTO "public"."tb_estoque_saida_produto" VALUES (5, 6, 8, 2, 'R$ 10,80');
INSERT INTO "public"."tb_estoque_saida_produto" VALUES (6, 10, 8, 30, 'R$ 10,80');
INSERT INTO "public"."tb_estoque_saida_produto" VALUES (7, 7, 8, 5, 'R$ 10,80');
INSERT INTO "public"."tb_estoque_saida_produto" VALUES (8, 9, 9, 15, 'R$ 10,80');
INSERT INTO "public"."tb_estoque_saida_produto" VALUES (9, 10, 9, 30, 'R$ 10,80');
INSERT INTO "public"."tb_estoque_saida_produto" VALUES (10, 7, 9, 5, 'R$ 10,80');

-- ----------------------------
-- Table structure for tb_produto
-- ----------------------------
DROP TABLE IF EXISTS "public"."tb_produto";
CREATE TABLE "public"."tb_produto" (
  "pro_id" int4 NOT NULL DEFAULT nextval('tb_produto_pro_id_seq'::regclass),
  "pro_nm" text COLLATE "pg_catalog"."default" NOT NULL,
  "pro_status" bool NOT NULL,
  "pro_dtcad" timestamp(6) NOT NULL,
  "pro_dtmod" timestamp(6) NOT NULL
)
;

-- ----------------------------
-- Records of tb_produto
-- ----------------------------
INSERT INTO "public"."tb_produto" VALUES (6, 'COCA COLA', 't', '2024-06-11 17:59:02.724305', '2024-06-11 17:59:02.724308');
INSERT INTO "public"."tb_produto" VALUES (7, 'AÇUCA', 't', '2024-06-11 17:59:26.751244', '2024-06-11 17:59:26.751247');
INSERT INTO "public"."tb_produto" VALUES (8, 'CAFE PRETO', 't', '2024-06-11 17:59:32.374971', '2024-06-11 17:59:32.374974');
INSERT INTO "public"."tb_produto" VALUES (9, 'ARROZ', 't', '2024-06-11 17:59:39.750048', '2024-06-11 17:59:39.75005');
INSERT INTO "public"."tb_produto" VALUES (10, 'FEIZAO', 't', '2024-06-11 17:59:44.710202', '2024-06-11 17:59:44.710204');
INSERT INTO "public"."tb_produto" VALUES (11, 'SABÃO EM PÓ', 't', '2024-06-11 19:54:51.168838', '2024-06-11 19:54:51.168839');

-- ----------------------------
-- Table structure for tb_produto_estoque
-- ----------------------------
DROP TABLE IF EXISTS "public"."tb_produto_estoque";
CREATE TABLE "public"."tb_produto_estoque" (
  "est_id" int4 NOT NULL DEFAULT nextval('tb_produto_estoque_est_id_seq'::regclass),
  "est_qtd" int4 NOT NULL,
  "est_qtd_atual" int4 NOT NULL,
  "est_dtvenc" date,
  "est_dtcad" timestamp(6) NOT NULL,
  "est_dtmod" timestamp(6) NOT NULL,
  "pro_id" int4 NOT NULL,
  "est_preco" money
)
;

-- ----------------------------
-- Records of tb_produto_estoque
-- ----------------------------
INSERT INTO "public"."tb_produto_estoque" VALUES (6, 130, 126, '2025-07-11', '2024-06-11 18:00:48.179946', '2024-06-11 18:00:48.179948', 6, 'R$ 10,80');
INSERT INTO "public"."tb_produto_estoque" VALUES (9, 130, 115, '2025-07-11', '2024-06-11 18:01:16.727744', '2024-06-11 18:01:16.727746', 7, 'R$ 7,80');
INSERT INTO "public"."tb_produto_estoque" VALUES (10, 130, 70, '2025-08-08', '2024-06-11 18:01:30.11636', '2024-06-11 18:01:30.116363', 9, 'R$ 7,80');
INSERT INTO "public"."tb_produto_estoque" VALUES (7, 130, 120, '2025-07-11', '2024-06-11 18:00:55.194834', '2024-06-11 18:00:55.194836', 6, 'R$ 12,80');
INSERT INTO "public"."tb_produto_estoque" VALUES (11, 10, 130, NULL, '2024-06-11 19:54:31.751527', '2024-06-11 19:54:31.751527', 10, 'R$ 7,80');
INSERT INTO "public"."tb_produto_estoque" VALUES (8, 130, 130, '2021-07-11', '2024-06-11 18:01:16.001312', '2024-06-11 18:01:16.001315', 7, 'R$ 7,80');

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
ALTER SEQUENCE "public"."tb_estoque_saida_produto_ite_id_seq"
OWNED BY "public"."tb_estoque_saida_produto"."ite_id";
SELECT setval('"public"."tb_estoque_saida_produto_ite_id_seq"', 10, true);

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
ALTER SEQUENCE "public"."tb_estoque_saida_sai_id_seq"
OWNED BY "public"."tb_estoque_saida"."sai_id";
SELECT setval('"public"."tb_estoque_saida_sai_id_seq"', 9, true);

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
ALTER SEQUENCE "public"."tb_produto_estoque_est_id_seq"
OWNED BY "public"."tb_produto_estoque"."est_id";
SELECT setval('"public"."tb_produto_estoque_est_id_seq"', 11, true);

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
ALTER SEQUENCE "public"."tb_produto_pro_id_seq"
OWNED BY "public"."tb_produto"."pro_id";
SELECT setval('"public"."tb_produto_pro_id_seq"', 12, true);

-- ----------------------------
-- Primary Key structure for table tb_estoque_saida
-- ----------------------------
ALTER TABLE "public"."tb_estoque_saida" ADD CONSTRAINT "pk6" PRIMARY KEY ("sai_id");

-- ----------------------------
-- Primary Key structure for table tb_estoque_saida_produto
-- ----------------------------
ALTER TABLE "public"."tb_estoque_saida_produto" ADD CONSTRAINT "pk8" PRIMARY KEY ("ite_id");

-- ----------------------------
-- Primary Key structure for table tb_produto
-- ----------------------------
ALTER TABLE "public"."tb_produto" ADD CONSTRAINT "pk1" PRIMARY KEY ("pro_id");

-- ----------------------------
-- Primary Key structure for table tb_produto_estoque
-- ----------------------------
ALTER TABLE "public"."tb_produto_estoque" ADD CONSTRAINT "pk4" PRIMARY KEY ("est_id");

-- ----------------------------
-- Foreign Keys structure for table tb_estoque_saida_produto
-- ----------------------------
ALTER TABLE "public"."tb_estoque_saida_produto" ADD CONSTRAINT "reftb_estoque_saida5" FOREIGN KEY ("sai_id") REFERENCES "public"."tb_estoque_saida" ("sai_id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."tb_estoque_saida_produto" ADD CONSTRAINT "reftb_produto_estoque6" FOREIGN KEY ("est_id") REFERENCES "public"."tb_produto_estoque" ("est_id") ON DELETE NO ACTION ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table tb_produto_estoque
-- ----------------------------
ALTER TABLE "public"."tb_produto_estoque" ADD CONSTRAINT "reftb_produto2" FOREIGN KEY ("pro_id") REFERENCES "public"."tb_produto" ("pro_id") ON DELETE NO ACTION ON UPDATE NO ACTION;
