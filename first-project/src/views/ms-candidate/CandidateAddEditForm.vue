<script setup lang="ts">
import { ref, watch } from 'vue';

const props = defineProps<{
  modelValue: any;
  mode: 'add' | 'edit';
}>();

const emit = defineEmits(['update:modelValue', 'save', 'cancel']);

const form = ref({
  name: '',
  phone: '',
  email: '',
  dob: '',
  gender: '',
  area: '',
  address: '',
  educationLevel: '',
  educationLocation: '',
  major: '',
  hiring_campaign: '',
  hiring_position: '',
  hiring_at: new Date().toISOString().substr(0, 10),
  candidateSource: '',
  recruiter: 'Đình Nga QTHT',
  collaborator: '',
  recentWorkplace: '',
  workplace: '',
  startDate: '',
  endDate: '',
  jobPosition: '',
  jobDescription: '',
  review: '',
});

watch(() => props.modelValue, (val) => {
  if (val) {
    form.value = { ...form.value, ...val };
  } else {
    // Reset form
    form.value = {
      name: '',
      phone: '',
      email: '',
      dob: '',
      gender: '',
      area: '',
      address: '',
      educationLevel: '',
      educationLocation: '',
      major: '',
      hiring_campaign: '',
      hiring_position: '',
      hiring_at: new Date().toISOString().substr(0, 10),
      candidateSource: '',
      recruiter: 'Đình Nga QTHT',
      collaborator: '',
      recentWorkplace: '',
      workplace: '',
      startDate: '',
      endDate: '',
      jobPosition: '',
      jobDescription: '',
      review: '',
    };
  }
}, { immediate: true });

const save = () => {
  emit('save', form.value);
};
</script>

<template>
  <div class="modal__body">
    <!-- Upload CV -->
    <div class="upload__container">
      <div class="upload__box">
        <div class="upload__icon">
          <i class="bi bi-cloud-arrow-up"></i>
        </div>
        <p class="upload__text">Kéo thả hoặc bấm vào đây để tải CV lên</p>
        <p class="upload__hint">Chấp nhận file .doc, .docx, .pdf, .jpg, .jpeg, .png (Dung lượng &lt; hơn 15 Mb)</p>
      </div>
    </div>

    <!-- Form Fields -->
    <form class="form__container" @submit.prevent="save">
      <div class="form__left">
        <div class="avatar__upload">
          <div class="avatar__placeholder">Ảnh</div>
        </div>
      </div>

      <div class="form__right">
        <!-- Họ tên -->
        <div class="form__row">
          <label>Họ và tên <span class="required">*</span></label>
          <input type="text" v-model="form.name" placeholder="Nhập họ và tên" required>
        </div>

        <!-- Ngày sinh, Giới tính -->
        <div class="form__row flex__row gap-16">
          <div class="flex__grow">
            <label>Ngày sinh <span class="sub__label">Ngày tháng năm</span></label>
            <div class="input__with__icon">
              <input type="text" v-model="form.dob" placeholder="dd/MM/yyyy">
              <i class="bi bi-calendar3"></i>
            </div>
          </div>
          <div class="flex__grow">
            <label>Giới tính</label>
            <select v-model="form.gender">
              <option value="">Chọn giới tính</option>
              <option value="Nam">Nam</option>
              <option value="Nữ">Nữ</option>
            </select>
          </div>
        </div>

        <!-- Khu vực -->
        <div class="form__row">
          <label>Khu vực</label>
          <div class="input__group">
            <select v-model="form.area" class="flex__grow">
              <option value="">Chọn giá trị</option>
              <option value="Ba Đình">Ba Đình</option>
              <option value="Hoàn Kiếm">Hoàn Kiếm</option>
              <option value="Hai Bà Trưng">Hai Bà Trưng</option>
            </select>
            <button type="button" class="btn__more">...</button>
          </div>
        </div>

        <!-- Sdt, email -->
        <div class="form__row flex__row gap-16">
          <div class="flex__grow">
            <label>Số điện thoại <span class="required">*</span></label>
            <input type="text" v-model="form.phone" placeholder="Nhập số điện thoại" required>
          </div>
          <div class="flex__grow">
            <label>Email <span class="required">*</span></label>
            <input type="email" v-model="form.email" placeholder="Nhập Email" required>
          </div>
        </div>

        <!-- Địa chỉ -->
        <div class="form__row">
          <label>Địa chỉ</label>
          <input type="text" v-model="form.address" placeholder="Nhập địa chỉ">
        </div>

        <div class="section__title">HỌC VẤN</div>

        <div class="education__section">
          <div class="form__row flex__row gap-8 align-center">
            <label class="edu__label__fixed">• Trình độ đào tạo</label>
            <div class="input__with__actions flex__grow">
              <input type="text" v-model="form.educationLevel" placeholder="Nhập trình độ đào tạo">
              <div class="actions">
                <i class="bi bi-plus"></i>
                <i class="bi bi-chevron-down"></i>
              </div>
            </div>
          </div>
          <div class="form__row flex__row gap-8 align-center">
            <label class="edu__label__fixed">• Nơi đào tạo</label>
            <div class="input__with__actions flex__grow">
              <input type="text" v-model="form.educationLocation" placeholder="Nhập nơi đào tạo">
              <div class="actions">
                <i class="bi bi-plus"></i>
                <i class="bi bi-chevron-down"></i>
              </div>
            </div>
          </div>
          <div class="form__row flex__row gap-8 align-center">
            <label class="edu__label__fixed">• Chuyên ngành</label>
            <div class="input__with__actions flex__grow">
              <input type="text" v-model="form.major" placeholder="Nhập chuyên ngành">
              <div class="actions">
                <i class="bi bi-plus"></i>
                <i class="bi bi-chevron-down"></i>
              </div>
            </div>
          </div>
          <a href="#" class="add__education__link">+ Thêm học vấn</a>
        </div>

        <hr class="divider">

        <!-- Ngày ứng tuyển, Nguồn ứng viên -->
        <div class="form__row flex__row gap-16">
          <div class="flex__grow">
            <label>Ngày ứng tuyển <span class="required">*</span></label>
            <div class="input__with__icon">
              <!-- Using type="date" for better UX in browser, though reference uses text -->
              <input type="date" v-model="form.hiring_at">
            </div>
          </div>
          <div class="flex__grow">
            <label>Nguồn ứng viên</label>
            <select v-model="form.candidateSource">
              <option value="">Chọn nguồn ứng viên</option>
            </select>
          </div>
        </div>

        <!-- Nhân sự khai thác + cộng tác viên -->
        <div class="form__row flex__row gap-16">
          <div class="flex__grow">
            <label>Nhân sự khai thác <span class="required">*</span></label>
            <select v-model="form.recruiter">
              <option value="Đình Nga QTHT">Đình Nga QTHT</option>
            </select>
          </div>
          <div class="flex__grow">
            <label>Cộng tác viên</label>
            <select v-model="form.collaborator">
              <option value="">Chọn cộng tác viên</option>
              <option value="A">A</option>
              <option value="B">B</option>
              <option value="C">C</option>
            </select>
          </div>
        </div>

        <div class="form__row" style="display: flex; align-items: center; gap: 8px;">
          <input type="checkbox" id="quickAdd" style="width: auto; height: auto;">
          <label for="quickAdd" style="margin-bottom: 0; font-weight: 400;">Thêm nhanh người tham chiếu vào kho ứng viên</label>
        </div>

        <div class="form__row">
          <a href="#" class="add__education__link">+ Thêm người giới thiệu</a>
          <div style="margin-top: 12px">
            <label>Nơi làm việc gần đây</label>
            <input type="text" v-model="form.recentWorkplace" placeholder="Nhập nơi làm việc gần đây">
          </div>
        </div>

        <hr class="divider">

        <div class="form__row">
          <a href="#" class="add__education__link">+ Thêm kinh nghiệm làm việc</a>
          <div style="margin-top: 12px;">
            <label>Nơi làm việc</label>
            <input type="text" v-model="form.workplace" placeholder="Nhập nơi làm việc">
          </div>
        </div>

        <div class="form__row flex__row gap-16">
          <div class="flex__grow">
            <label>Thời gian bắt đầu <span class="required">*</span></label>
            <div class="input__with__icon">
              <input type="text" v-model="form.startDate" placeholder="dd/MM/yyyy">
              <i class="bi bi-calendar3"></i>
            </div>
          </div>
          <div style="padding-bottom: 10px;">-</div>
          <div class="flex__grow">
            <label>Thời gian kết thúc</label>
            <div class="input__with__icon">
              <input type="text" v-model="form.endDate" placeholder="dd/MM/yyyy">
              <i class="bi bi-calendar3"></i>
            </div>
          </div>
        </div>

        <div class="form__row">
          <label>Vị trí công việc</label>
          <input type="text" v-model="form.jobPosition" placeholder="Nhập vị trí công việc">
        </div>

        <div class="form__row">
          <label>Mô tả công việc</label>
          <textarea v-model="form.jobDescription" placeholder="Nhập mô tả công việc"></textarea>
        </div>
      </div>
    </form>
  </div>
  <div class="modal__footer">
    <button type="button" class="btn__cancel" @click="$emit('cancel')">Hủy</button>
    <button type="submit" class="btn__save" @click="save">
      {{ mode === 'add' ? 'Lưu' : 'Cập nhật' }}
    </button>
  </div>
</template>

<style scoped>
.form__container {
  display: flex;
  gap: 24px;
  align-items: flex-start;
}

.form__left {
  flex-shrink: 0;
  width: 100px;
  display: flex;
  justify-content: center;
}

.form__right {
  flex-grow: 1;
}

.modal__body {
  padding: 16px 24px;
  overflow-y: auto;
  flex-grow: 1;
}

.modal__footer {
  padding: 12px 24px;
  border-top: 1px solid #f0f0f0;
  display: flex;
  justify-content: flex-end;
  gap: 8px;
  background-color: #f8f9fa;
}

/* Upload CV Area */
.upload__container {
  margin-bottom: 24px;
}

.upload__box {
  border: 1px dashed #2970f6;
  border-radius: 4px;
  padding: 20px;
  text-align: center;
  background-color: #f1f6ff;
  cursor: pointer;
  transition: all 0.2s ease;
  display: flex;
  flex-direction: column;
  gap: 4px;
}

.upload__box:hover {
  background-color: #e8f0fe;
  border-color: #2159c4;
}

.upload__icon {
  font-size: 32px;
  color: #2970f6;
  margin-bottom: 4px;
}

.upload__text {
  color: #2970f6;
  margin: 0;
  font-weight: 600;
  font-size: 14px;
}

.upload__hint {
  color: #65696e;
  font-size: 12px;
  margin: 0;
}

/* Form Groups */
.form__row {
  margin-bottom: 16px;
}

.flex__row {
  display: flex;
  align-items: flex-end;
}

.gap-8 { gap: 8px; }
.gap-16 { gap: 16px; }

.flex__grow {
  flex-grow: 1;
  max-width: calc(50% - 8px);
}

label {
  display: block;
  font-size: 14px;
  font-weight: 600;
  margin-bottom: 8px;
  color: #212121;
}

.sub__label {
  font-weight: 400;
  color: #65696e;
  margin-left: 4px;
  font-size: 13px;
}

.required {
  color: #ff4d4f;
  margin-left: 2px;
}

input[type="text"],
input[type="email"],
input[type="date"],
select,
textarea {
  width: 100%;
  height: 36px;
  padding: 8px 12px;
  border: 1px solid #dddde4;
  border-radius: 4px;
  font-size: 14px;
  outline: none;
  background-color: #fff;
  font-family: inherit;
}

textarea {
  height: 80px;
  resize: vertical;
}

input:focus,
select:focus,
textarea:focus {
  border-color: #2970f6;
}

.input__with__icon {
  position: relative;
  display: flex;
  align-items: center;
}

.input__with__icon i {
  position: absolute;
  right: 10px;
  color: #65696e;
  pointer-events: none;
}

.input__group {
  display: flex;
  gap: 4px;
}

.btn__more {
  width: 36px;
  height: 36px;
  border: 1px solid #dddde4;
  background-color: #f8f9fa;
  border-radius: 4px;
  cursor: pointer;
  font-weight: bold;
}

/* Avatar Placeholder */
.avatar__placeholder {
  width: 80px;
  height: 80px;
  border: 1px dashed #dddde4;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  color: #65696e;
  font-size: 13px;
  background-color: #fff;
}

.section__title {
  font-size: 13px;
  font-weight: 700;
  color: #212121;
  margin: 24px 0 16px 0;
}

.education__section {
  padding-left: 8px;
}

.edu__label__fixed {
  width: 150px;
  margin-bottom: 0;
  font-weight: 600;
}

.input__with__actions {
  position: relative;
}

.input__with__actions input {
  padding-right: 60px;
}

.input__with__actions .actions {
  position: absolute;
  right: 8px;
  top: 50%;
  transform: translateY(-50%);
  display: flex;
  gap: 8px;
  color: #2970f6;
}

.input__with__actions .actions i {
  cursor: pointer;
}

.add__education__link {
  display: inline-block;
  margin-top: 8px;
  color: #2970f6;
  text-decoration: none;
  font-size: 14px;
  font-weight: 500;
  cursor: pointer;
}

.divider {
  border: none;
  border-top: 1px solid #f0f0f0;
  margin: 24px 0;
}

.btn__cancel {
  height: 36px;
  padding: 0 16px;
  border: 1px solid transparent;
  background: transparent;
  color: #212121;
  cursor: pointer;
  font-weight: 500;
  border-radius: 4px;
}

.btn__cancel:hover {
  background-color: rgba(0, 0, 0, 0.05);
}

.btn__save {
  height: 36px;
  padding: 0 24px;
  border: none;
  background-color: #2970f6;
  color: #fff;
  cursor: pointer;
  font-weight: 500;
  border-radius: 4px;
}

.btn__save:hover {
  background-color: #2159c4;
}
</style>
