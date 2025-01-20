namespace Demo01.Models
{
    // 모델 클래스 정의
    public class FeedbackModel
    {
        public string Name { get; set; }
        public string Query { get; set; }
        public bool IsRecommended { get; set; }
    }

    /*
     CREATE TABLE feedback (
        id INT AUTO_INCREMENT PRIMARY KEY, -- 기본 키, 자동 증가
        name VARCHAR(255) NOT NULL,        -- 고객명
        query TEXT NOT NULL,               -- 문의글
        is_recommended BOOLEAN NOT NULL,   -- 추천 여부
        memo VARCHAR(255) NULL,            -- 메모 (NULL 허용)
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP -- 생성 시간 (자동 추가)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
    */
}
