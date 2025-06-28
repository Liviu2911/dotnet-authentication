import { Link, createFileRoute } from '@tanstack/react-router'
import Form from '@/components/form'
import Input from '@/components/form/input'

export const Route = createFileRoute('/register')({
  component: RouteComponent,
})

function RouteComponent() {
  return (
    <Form>
      <h1 className="text-4xl text-primary absolute top-48 uppercase font-bold">
        Create Account
      </h1>
      <Input name="username" />
      <Input name="email" />
      <Input name="password" />
      <Input name="retype password" />
      <div className="flex flex-col gap-2 items-center">
        <button className="mt-4 font-medium cursor-pointer px-3 py-1 rounded bg-primary hover:opacity-80 transition-all">
          Create Account
        </button>
        <span className="opacity-80">or</span>
        <Link
          to="/login"
          className="opacity-80 hover:opacity-100 transition-all"
        >
          Log In
        </Link>
      </div>
    </Form>
  )
}
