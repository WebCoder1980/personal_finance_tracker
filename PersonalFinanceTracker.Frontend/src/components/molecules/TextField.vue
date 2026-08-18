<script setup lang="ts">

import {Input} from "@/components/ui/input";

defineProps({
  title: {
    type: String,
    required: true
  },
  type: {
    type: String
  }
})
const model = defineModel<string>()
</script>

<template>
  <div class="flex flex-col w-full">
    <span>{{title}}: </span>
    <Input v-model="model" :type="type"></Input>
  </div>
</template>

<style scoped>

</style>