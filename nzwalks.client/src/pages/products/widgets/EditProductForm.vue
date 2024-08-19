<script setup lang="ts">
import { PropType, computed, ref, watch } from 'vue'
import { useForm } from 'vuestic-ui'
import { Product } from '../types'
import { validators } from '../../../services/utils'

const props = defineProps({
  product: {
    type: Object as PropType<Product | null>,
    default: null,
  },
  saveButtonLabel: {
    type: String,
    default: 'Save',
  },
})

const defaultNewProduct: Product = {
  id: -1,
  name: '',
  slug: '',
  description: '',
  sku: '',
  price: '',
  stock: '',
  image_url: '',
  category_id: '',
}

  const newProduct = ref<Product>({ ...defaultNewProduct })

const isFormHasUnsavedChanges = computed(() => {
  return Object.keys(newProduct.value).some((key) => {
    return newProduct.value[key as keyof Product] !== (props.product ?? defaultNewProduct)?.[key as keyof Product]
  })
})

defineExpose({
  isFormHasUnsavedChanges,
})

watch(
  () => props.product,
  () => {
    if (!props.product) {
      return
    }

    newProduct.value = {
      ...props.product,
    }
  },
  { immediate: true },
)

const form = useForm('add-product-form')

const emit = defineEmits(['close', 'save'])

const onSave = () => {
  if (form.validate()) {
    emit('save', newProduct.value)
  }
}
</script>

<template>
  <VaForm v-slot="{ isValid }" ref="add-product-form" class="flex-col justify-start items-start gap-4 inline-flex w-full">
    <div class="self-stretch flex-col justify-start items-start gap-4 flex">
      <div class="flex gap-4 flex-col sm:flex-row w-full">
        <VaInput
          v-model="newProduct.name"
          label="Name"
          class="w-full sm:w-1/2"
          :rules="[validators.required]"
          name="name"
        />
        <VaInput
          v-model="newProduct.slug"
          label="Slug"
          class="w-full sm:w-1/2"
          :rules="[validators.required]"
          name="slug"
        />
      </div>
      <div class="flex gap-2 flex-col-reverse items-stretch justify-end w-full sm:flex-row sm:items-center">
        <VaButton preset="secondary" color="secondary" @click="$emit('close')">Cancel</VaButton>
        <VaButton :disabled="!isValid" @click="onSave">{{ saveButtonLabel }}</VaButton>
      </div>
    </div>
  </VaForm>
</template>
