import { sleep } from '../../services/utils'
import { Product } from './../../pages/products/types'

export let products = [] as Product[]

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

export type Filters = {
  isActive: boolean
  search: string
}

const getSortItem = (obj: any, sortBy: string) => {
  return obj[sortBy]
}

export const getProducts = async (filters: Partial<Filters & Pagination & Sorting>) => {
  await sleep(1000)
  const { isActive, search, sortBy, sortingOrder } = filters
  const response = await fetch('api/Products?pageNumber=' + filters.page + '&pageSize=' + filters.perPage)
  const metadata = JSON.parse((await response.headers.get('x-pagination')) || '{}')
  let filteredProducts = (await response.json()) as Product[]

  filteredProducts = filteredProducts.filter((product) => product.active === isActive)

  if (search) {
    filteredProducts = filteredProducts.filter((product) => product.name.toLowerCase().includes(search.toLowerCase()))
  }

  if (sortBy && sortingOrder) {
    filteredProducts = filteredProducts.sort((a, b) => {
      const first = getSortItem(a, sortBy)
      const second = getSortItem(b, sortBy)
      if (first > second) {
        return sortingOrder === 'asc' ? 1 : -1
      }
      if (first < second) {
        return sortingOrder === 'asc' ? -1 : 1
      }
      return 0
    })
  }

  const { page = 1, perPage = 10 } = filters || {}
  return {
    data: (products = filteredProducts),
    pagination: {
      page,
      perPage,
      total: metadata?.TotalItemCount || filteredProducts.length,
    },
  }
}

export const addProduct = async (product: Product) => {
  await sleep(1000)
  const requestOptions = {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Authorization: 'Bearer my-token',
    },
    body: JSON.stringify(product),
  }
  const response = await fetch('api/Products', requestOptions)
  const newProduct = (await response.json()) as Product
  products.unshift(newProduct)
}

export const updateProduct = async (product: Product) => {
  await sleep(1000)
  const requestOptions = {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
      Authorization: 'Bearer my-token',
    },
    body: JSON.stringify(product),
  }
  const response = await fetch('api/Products/' + product.id, requestOptions)
  const existingProduct = (await response.json()) as Product
  const index = products.findIndex((x) => x.id === product.id)
  products[index] = existingProduct
}

export const removeProduct = async (product: Product) => {
  await sleep(1000)
  await fetch('api/Products/' + product.id, { method: 'DELETE' })
  products.splice(
    products.findIndex((x) => x.id === product.id),
    1,
  )
}
