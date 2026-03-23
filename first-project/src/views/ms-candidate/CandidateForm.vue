<script setup lang="ts">
import { ref, watch } from 'vue';

const props = defineProps<{
  modelValue: any;
  mode: 'add' | 'edit' | 'view';
}>();

const emit = defineEmits(['update:modelValue', 'save', 'cancel']);

const form = ref({
  name: '',
  phone: '',
  email: '',
  hiring_campaign: '',
  hiring_position: '',
  hiring_at: new Date().toISOString().substr(0, 10),
  review: '',
});

watch(() => props.modelValue, (val) => {
  if (val) {
    form.value = { ...val };
  } else {
    form.value = {
      name: '',
      phone: '',
      email: '',
      hiring_campaign: '',
      hiring_position: '',
      hiring_at: new Date().toISOString().substr(0, 10),
      review: '',
    };
  }
}, { immediate: true });

const save = () => {
  emit('save', form.value);
};
</script>

<template>
  <v-container>
    <v-row>
      <v-col cols="12" sm="6">
        <v-text-field
          v-model="form.name"
          label="Họ và Tên"
          :readonly="mode === 'view'"
          variant="outlined"
          density="comfortable"
        ></v-text-field>
      </v-col>
      <v-col cols="12" sm="6">
        <v-text-field
          v-model="form.phone"
          label="Số điện thoại"
          :readonly="mode === 'view'"
          variant="outlined"
          density="comfortable"
        ></v-text-field>
      </v-col>
      <v-col cols="12" sm="6">
        <v-text-field
          v-model="form.email"
          label="Email"
          :readonly="mode === 'view'"
          variant="outlined"
          density="comfortable"
        ></v-text-field>
      </v-col>
      <v-col cols="12" sm="6">
        <v-text-field
          v-model="form.hiring_at"
          label="Ngày ứng tuyển"
          type="date"
          :readonly="mode === 'view'"
          variant="outlined"
          density="comfortable"
        ></v-text-field>
      </v-col>
      <v-col cols="12" sm="6">
        <v-text-field
          v-model="form.hiring_campaign"
          label="Chiến dịch tuyển dụng"
          :readonly="mode === 'view'"
          variant="outlined"
          density="comfortable"
        ></v-text-field>
      </v-col>
      <v-col cols="12" sm="6">
        <v-text-field
          v-model="form.hiring_position"
          label="Vị trí tuyển dụng"
          :readonly="mode === 'view'"
          variant="outlined"
          density="comfortable"
        ></v-text-field>
      </v-col>
      <v-col cols="12">
        <v-textarea
          v-model="form.review"
          label="Đánh giá"
          :readonly="mode === 'view'"
          variant="outlined"
          density="comfortable"
          rows="3"
        ></v-textarea>
      </v-col>
    </v-row>
    <v-card-actions class="px-0">
      <v-spacer></v-spacer>
      <v-btn color="grey-darken-1" variant="text" @click="$emit('cancel')">Hủy</v-btn>
      <v-btn v-if="mode !== 'view'" color="primary" variant="elevated" @click="save">Lưu</v-btn>
    </v-card-actions>
  </v-container>
</template>
