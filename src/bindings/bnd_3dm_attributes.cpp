#include "bindings.h"

BND_3dmAttributes::BND_3dmAttributes()
{
  SetTrackedPointer(new ON_3dmObjectAttributes(), nullptr);
}

BND_3dmAttributes::BND_3dmAttributes(ON_3dmObjectAttributes* attrs, const ON_ModelComponentReference* compref)
{
  SetTrackedPointer(attrs, compref);
}

void BND_3dmAttributes::SetTrackedPointer(ON_3dmObjectAttributes* attrs, const ON_ModelComponentReference* compref)
{
  m_attributes = attrs;
  BND_Object::SetTrackedPointer(attrs, compref);
}

BND_UUID BND_3dmAttributes::GetObjectId() const
{
  return ON_UUID_to_Binding(m_attributes->m_uuid);
}

std::wstring BND_3dmAttributes::GetName() const
{
  return std::wstring(m_attributes->m_name);
}

void BND_3dmAttributes::SetName(const std::wstring name)
{
  m_attributes->m_name = name.c_str();
}

//////////////////////////////////////////////////////////////////////////////////////

#if defined(ON_PYTHON_COMPILE)
namespace py = pybind11;
void init3dmAttributesBindings(pybind11::module& m)
{
  py::class_<BND_3dmAttributes, BND_Object>(m, "ObjectAttributes")
    .def(py::init<>())
    .def_property("Visible", &BND_3dmAttributes::IsVisible, &BND_3dmAttributes::SetVisible)
    .def_property("Name", &BND_3dmAttributes::GetName, &BND_3dmAttributes::SetName)
    .def_property_readonly("Id", &BND_3dmAttributes::GetObjectId)
    .def_property("LayerIndex", &BND_3dmAttributes::GetLayerIndex, &BND_3dmAttributes::SetLayerIndex)
    ;
}
#endif

#if defined(ON_WASM_COMPILE)
using namespace emscripten;

void init3dmAttributesBindings(void*)
{
  class_<BND_3dmAttributes, base<BND_Object>>("ObjectAttributes")
    .constructor<>()
    .property("visible", &BND_3dmAttributes::IsVisible, &BND_3dmAttributes::SetVisible)
    .property("name", &BND_3dmAttributes::GetName, &BND_3dmAttributes::SetName)
    .property("id", &BND_3dmAttributes::GetObjectId)
    .property("layerIndex", &BND_3dmAttributes::GetLayerIndex, &BND_3dmAttributes::SetLayerIndex)
  ;

}
#endif
