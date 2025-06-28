import { Link, createFileRoute } from '@tanstack/react-router'
import Form from '@/components/form'
import Input from '@/components/form/input'

export const Route = createFileRoute('/login')({
  component: RouteComponent,
})

function RouteComponent() {
  return (
    <Form>
      <h1 className="text-3xl font-bold uppercase absolute top-64 text-primary">
        Log In
      </h1>
      <Input name="email" />
      <Input name="password" />
      <div className="mt-4 flex flex-col gap-2 items-center">
        <button className="bg-primary px-3 py-1 rounded font-medium cursor-pointer hover:opacity-90 transition-all">
          Log In
        </button>
        <span className="opacity-80">or</span>
        <Link
          to="/register"
          className="opacity-80 hover:opacity-100 transition-all"
        >
          Create Account
        </Link>
      </div>
    </Form>
  )
}
