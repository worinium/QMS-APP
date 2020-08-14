<template>
  <v-footer color="#43a047" dark padless fixed>
    <v-row no-gutters>
      <v-btn v-for="link in links" :key="link" color="white" text rounded class="my-2">
        {{ link }}
      </v-btn>
      <v-col class="success lighten-2 py-4 text-center white--text" cols="12">
       &copy; {{ new Date().getFullYear() }} — <strong>Worisoft Inc.</strong>
      </v-col>
    </v-row>
  </v-footer>
</template>
<script>
  export default {
    data: () => ({ links: ['Home','About Us','Team','Services','Blog','Contact Us',], }),
    methods:{


    }
  }
</script>
