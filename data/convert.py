import re
import pandas as pd

def extract_vocabulary_from_file(file_path):
    with open(file_path, "r", encoding="utf-8") as f:
        lines = f.readlines()

    data = []
    current_topic = None
    pattern = re.compile(r"^(.*?)\s+/([^/]+)/\s+(.*)$")

    for line in lines:
        line = line.strip()
        # Nhận diện chủ đề
        if re.match(r"^\d+\.\s*Từ vựng về", line, flags=re.IGNORECASE):
            current_topic = line.split('.', 1)[1].strip()
        # Nhận diện dòng có từ, phát âm và nghĩa
        elif match := pattern.match(line):
            vocab, pronunciation, meaning = match.groups()
            data.append({
                "Từ vựng": vocab.strip(),
                "Phát âm": f"/{pronunciation.strip()}/",
                "Nghĩa": meaning.strip(),
                "Chủ đề": current_topic
            })

    return pd.DataFrame(data)

# Sử dụng hàm
file_path = "1000 từ vựng tiếng Anh theo chủ đề.txt"
df = extract_vocabulary_from_file(file_path)

# Hiển thị vài dòng đầu
print(df.head())

# Lưu ra file nếu cần
df.to_csv("tu_vung_theo_chu_de.csv", index=False)
