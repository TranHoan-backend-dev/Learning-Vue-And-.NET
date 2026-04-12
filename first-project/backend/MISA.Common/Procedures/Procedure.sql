drop table if exists `Candidate`;
create table `Candidate`
(
    CandidateId    varchar(36) primary key,
    Name           varchar(255) not null,
    Phone          varchar(11)  not null unique,
    Email          varchar(255)  not null unique,
    HiringCampaign varchar(255),
    HiringPosition varchar(255) not null,
    HiringRound    varchar(255),
    Review         varchar(255),
    HiringAt       date,
    Status         integer(1)   not null default false,
    CreatedBy      varchar(255) not null,
    CreatedAt      datetime     not null,
    ModifiedBy     varchar(255) not null,
    ModifiedAt     datetime     not null
);

# delimiter //
# create procedure Proc_Candidate_Insert(
#     in @CandidateId_0 varchar(36),
#     in @Name_0 varchar(255),
#     in @Phone_0 varchar(11),
#     in @Email_0 varchar(255),
#     in @HiringCampaign_0 varchar(255),
#     in @HiringPosition_0 varchar(255),
#     in @HiringRound_0 varchar(255),
#     in @Review_0 varchar(255),
#     in @HiringAt_0 date,
#     in @Status_0 integer(1),
#     in @CreatedBy_0 varchar(255),
#     in @CreatedAt_0 datetime,
#     in @ModifiedBy_0 varchar(255),
#     in @ModifiedAt_0 datetime
# )
# begin
#     INSERT INTO `Candidate` (CandidateId, Name, Phone, Email, HiringCampaign, HiringPosition, HiringRound, Review,
#                              HiringAt, Status, CreatedBy, CreatedAt, ModifiedBy, ModifiedAt)
#     VALUES (@CandidateId_0, @Name_0, @Phone_0, @Email_0, @HiringCampaign_0, @HiringPosition_0, @HiringRound_0, @Review_0,
#             @HiringAt_0, @Status_0, @CreatedBy_0, @CreatedAt_0, @ModifiedBy_0, @ModifiedAt_1)
#     ON DUPLICATE KEY
#         end
#     //
# delimiter ;