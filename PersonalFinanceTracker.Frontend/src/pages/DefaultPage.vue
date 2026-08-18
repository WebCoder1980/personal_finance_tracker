<script setup lang="ts">
import {useAuthStore} from "@/service/AuthStore.ts";
import {useRouter} from "vue-router";

const router = useRouter()
const authStore = useAuthStore()
if (authStore.data == null) {
  router.push("/login")
}
else {
  router.push("/transactions")
}
</script>

<template>

</template>