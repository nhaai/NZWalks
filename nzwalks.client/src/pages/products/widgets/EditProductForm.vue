<script setup lang="ts">
import { PropType, computed, ref, watch } from 'vue'
import { useForm } from 'vuestic-ui'
import { Product } from '../types'
import { useCategories } from '../../categories/composables/useCategories'
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
  code: '',
  description: '',
  brand: '',
  unitPrice: '',
  quantity: '',
  purchases: '',
  views: '',
  imageUrl: '',
  active: true,
  categoryId: '',
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

const { categories } = useCategories({ pagination: ref({ page: 1, perPage: 9999 }) })
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
          v-model="newProduct.code"
          label="Code"
          class="w-full sm:w-1/2"
          name="code"
        />
      </div>
      <div class="flex gap-4 flex-col sm:flex-row w-full">
        <VaInput
          v-model="newProduct.brand"
          label="Brand"
          class="w-full sm:w-1/2"
          name="brand"
        />
        <VaInput
          v-model="newProduct.imageUrl"
          label="Image URL"
          class="w-full sm:w-1/2"
          name="imageUrl"
        />
      </div>
      <div class="flex gap-4 flex-col sm:flex-row w-full">
        <VaInput
          v-model="newProduct.unitPrice"
          label="Unit Price"
          class="w-full sm:w-1/2"
          :rules="[validators.required]"
          name="unitPrice"
        />
        <VaInput
          v-model="newProduct.quantity"
          label="Quantity In Stock"
          class="w-full sm:w-1/2"
          :rules="[validators.required]"
          name="quantity"
        />
      </div>
      <div class="flex gap-4 flex-col sm:flex-row w-full">
        <VaInput
          v-model="newProduct.purchases"
          label="Total Purchases"
          class="w-full sm:w-1/2"
          name="purchases"
        />
        <VaInput
          v-model="newProduct.views"
          label="Total Views"
          class="w-full sm:w-1/2"
          name="views"
        />
      </div>
      <div class="flex gap-4 w-full">
        <div class="w-1/2">
          <VaSelect
            v-model="newProduct.categoryId"
            label="Category"
            class="w-ful"
            :options="categories"
            :rules="[validators.required]"
            name="categoryId"
            text-by="name"
            value-by="id"
          />
        </div>
        <div class="flex items-center w-1/2 mt-4">
          <VaCheckbox v-model="newProduct.active" label="Active" class="w-full" name="active" />
        </div>
      </div>
      <VaTextarea v-model="newProduct.description" label="Description" class="w-full" name="description" />
      <div class="flex gap-2 flex-col-reverse items-stretch justify-end w-full sm:flex-row sm:items-center">
        <VaButton preset="secondary" color="secondary" @click="$emit('close')">Cancel</VaButton>
        <VaButton :disabled="!isValid" @click="onSave">{{ saveButtonLabel }}</VaButton>
      </div>
    </div>
  </VaForm>
</template>
