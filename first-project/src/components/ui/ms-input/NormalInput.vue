<script setup lang="ts">

const props = defineProps<{
  label?: string,
  placeholder: string,
  className?: string,
  type?: 'text' | 'tel' | 'checkbox',
}>()

const value = defineModel<string>({
  default: ''
})
</script>

<template>
  <v-text-field
      :label="props.label"
      :placeholder="props.placeholder"
      v-model="value"
      :aria-label="props.label"
      error-messages="string"
      :type="props.type ?? 'text'"
  ></v-text-field>
</template>

<style scoped>
:deep(.v-field) {
  border-radius: 10px;
}
</style>