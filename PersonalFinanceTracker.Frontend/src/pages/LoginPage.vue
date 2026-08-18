<script setup lang="ts">

import {login} from "@/service/Client.ts";
import {Button} from "@/components/ui/button";
import {useAuthStore} from "@/service/AuthStore.ts";
import {useRouter} from "vue-router";
import {Input} from "@/components/ui/input";
import {ref} from "vue";
import {Alert, AlertTitle, AlertDescription} from "@/components/ui/alert";
import TextField from "@/components/molecules/TextField.vue";

const router = useRouter()
const authStore = useAuthStore()
if (authStore.data != null) {
  router.push("/transactions")
}
const curLogin = ref("")
const curPassword = ref("")
const errorVisibility = ref(false)

async function loginButtonClicked() {
  try {
    await login(curLogin.value, curPassword.value);
  }
  catch (error) {
    console.log(error)
    errorVisibility.value = true
    return
  }
  router.push('/')
}
</script>

<template>
  <div class="flex items-center justify-center min-h-screen flex-col gap-3 px-[40%]">
    <div>Вы не авторизованы...</div>
    <TextField title="Логин" v-model="curLogin"></TextField>
    <TextField type="password" title="Пароль" v-model="curPassword"></TextField>
    <Button @click="async () => await loginButtonClicked()">Авторизация</Button>
    <Alert variant="destructive" v-if="errorVisibility">
      <AlertTitle>Ошибка при входе!</AlertTitle>
      <AlertDescription>
        Проверьте правильность логина или пароля.
      </AlertDescription>
    </Alert>
    <RouterLink to="/register">Нет аккаунта?</RouterLink>
  </div>
</template>