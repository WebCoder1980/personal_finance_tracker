<script setup lang="ts">
import {Input} from "@/components/ui/input";
import {useAuthStore} from "@/service/AuthStore.ts";
import {Button} from "@/components/ui/button";
import {useRouter} from "vue-router";

const authStore = useAuthStore()
const router = useRouter()
if (authStore.data == null) {
  router.push("/login")
}
</script>

<template>
  <div class="flex items-center justify-center min-h-screen flex-col gap-3 px-[40%]">
  <div>Вы авторизованы</div>
  <div class="flex flex-col w-full">
    <span>Токен:</span>
    <Input :model-value="authStore.data?.token" readonly />
  </div>

  <div class="flex flex-col w-full">
    <span>Имя:</span>
    <Input :model-value="authStore.data?.userName" readonly />
  </div>

  <div class="flex flex-col w-full">
    <span>Роль:</span>
    <Input :model-value="authStore.data?.role" readonly />
  </div>
  <Button @click="() => {authStore.data = null; router.push('/')}">Выход</Button>
  </div>
</template>
