import { sleep } from '../../services/utils'
import { Product } from '../../pages/producs/types'
import { createRequest } from '../../services/apiService'
// Simulate API calls

export type Pagination = {
  page: number
  perPage: number
  total: number
}
export type Sorting = {
  sortBy: keyof Product | undefined
  sortingOrder: 'asc' | 'desc' | null
}

const getSortItem = (obj: any, sortBy: keyof Product) => {
  if (sortBy === 'name') {
    return obj.name
  }

  if (sortBy === 'createat') {
    return new Date(obj[sortBy])
  }

  return obj[sortBy]
}
export const getProducts = async (options: Sorting & Pagination) => {
  await sleep(1000) // Simulate network delay

  try {
    const productsResponse = await createRequest({
      api: {
        url: 'https://localhost:7216/api/Products', // Replace with your API URL
        method: 'GET',
        params: {
          page: options.page,
          perPage: options.perPage,
          sortBy: options.sortBy,
          sortingOrder: options.sortingOrder,
        },
      },
    })

    const products: Product[] = productsResponse.data.map((product: any) => ({
      id: product.id,
      name: product.name,
      description: product.description,
      sku: product.sku,
      price: product.price,
      stock: product.stock,
      imageurl: product.imageurl,
      createat: new Date(product.createat),
      updateat: new Date(product.updateat),
    }))

    return {
      data: products,
      pagination: {
        page: options.page,
        perPage: options.perPage,
        total: productsResponse.total,
      },
    }
  } catch (error) {
    console.error('Error fetching products:', error)
    throw error
  }
}
