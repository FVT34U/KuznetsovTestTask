PGDMP         2                {            KuznetsovTestDatabase    15.2    15.2     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    24636    KuznetsovTestDatabase    DATABASE     �   CREATE DATABASE "KuznetsovTestDatabase" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
 '   DROP DATABASE "KuznetsovTestDatabase";
                postgres    false            �            1259    24638    EngineComponents    TABLE     �   CREATE TABLE public."EngineComponents" (
    "Id" integer NOT NULL,
    "Name" text,
    "Value" integer,
    "ParentId" integer,
    "NestingLevel" integer NOT NULL
);
 &   DROP TABLE public."EngineComponents";
       public         heap    postgres    false            �            1259    24637    EngineComponents_Id_seq    SEQUENCE     �   CREATE SEQUENCE public."EngineComponents_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE public."EngineComponents_Id_seq";
       public          postgres    false    215            �           0    0    EngineComponents_Id_seq    SEQUENCE OWNED BY     Y   ALTER SEQUENCE public."EngineComponents_Id_seq" OWNED BY public."EngineComponents"."Id";
          public          postgres    false    214            e           2604    24641    EngineComponents Id    DEFAULT     �   ALTER TABLE ONLY public."EngineComponents" ALTER COLUMN "Id" SET DEFAULT nextval('public."EngineComponents_Id_seq"'::regclass);
 F   ALTER TABLE public."EngineComponents" ALTER COLUMN "Id" DROP DEFAULT;
       public          postgres    false    214    215    215            �          0    24638    EngineComponents 
   TABLE DATA           _   COPY public."EngineComponents" ("Id", "Name", "Value", "ParentId", "NestingLevel") FROM stdin;
    public          postgres    false    215   �       �           0    0    EngineComponents_Id_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('public."EngineComponents_Id_seq"', 29, true);
          public          postgres    false    214            g           2606    24645 &   EngineComponents EngineComponents_pkey 
   CONSTRAINT     j   ALTER TABLE ONLY public."EngineComponents"
    ADD CONSTRAINT "EngineComponents_pkey" PRIMARY KEY ("Id");
 T   ALTER TABLE ONLY public."EngineComponents" DROP CONSTRAINT "EngineComponents_pkey";
       public            postgres    false    215            �   �   x�u�;�@Dk�� � �%8A�@C����h��'R��p��7��Xig��G8�OY�F+;rT�h�s�M'��/)�SB�����z����p6)[���f��ڟ�'�o�R"L���޾��^9��X)?��	W�(s�����cL20����Q��Js����M���b��_�r�'     