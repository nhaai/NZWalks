<script setup lang="ts">
  import { PropType, computed, toRef } from 'vue';
  import { defineVaDataTableColumns, useModal } from 'vuestic-ui'
  import { Product } from '../types'
  import { useVModel } from '@vueuse/core'

  const columns = defineVaDataTableColumns([
    { label: 'product name', key: 'name', sortable: true },
    { label: 'Description', key: 'description', sortable: true },
    { label: 'Sku', key: 'sku', sortable: false },
    { label: 'Price', key: 'price', sortable: true },
    { label: 'stock', key: 'stock', sortable: true },
    { label: 'Image', key: 'imageurl', sortable: true },
    { label: 'Create At', key: 'createat', sortable: true },
    { label: 'Update At', key: 'updateat', sortable: true },
  ])


  const props = defineProps({
    products: {
      type: Array as PropType<Product[]>,
      required: true,
    },
    loading: {
      type: Boolean,
      required: true,
    },
    sortBy: {
      type: Object as PropType<Sorting['sortBy']>,
      required: true,
    },
    sortingOrder: {
      type: Object as PropType<Sorting['sortingOrder']>,
      required: true,
    },
    pagination: {
      type: Object as PropType<Pagination>,
      required: true,
    },
  })

  const emit = defineEmits<{
    (event: 'edit', product: Product): void
    (event: 'delete', product: Product): void
  }>()
  const sortByVModel = useVModel(props, 'sortBy', emit)
  const sortingOrderVModel = useVModel(props, 'sortingOrder', emit)
  const totalPages = computed(() => Math.ceil(props.pagination.total / props.pagination.perPage))
</script>

<style>
</style>
