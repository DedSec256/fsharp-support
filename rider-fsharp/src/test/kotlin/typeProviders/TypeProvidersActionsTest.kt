package typeProviders

import com.jetbrains.rd.util.lifetime.Lifetime
import com.jetbrains.rdclient.testFramework.waitForDaemon
import com.jetbrains.rdclient.util.idea.waitAndPump
import com.jetbrains.rider.daemon.util.hasErrors
import com.jetbrains.rider.plugins.fsharp.rdFSharpModel
import com.jetbrains.rider.projectView.solution
import com.jetbrains.rider.test.annotations.TestEnvironment
import com.jetbrains.rider.test.asserts.shouldBeFalse
import com.jetbrains.rider.test.asserts.shouldBeNull
import com.jetbrains.rider.test.asserts.shouldNotBeNull
import com.jetbrains.rider.test.base.BaseTestWithSolution
import com.jetbrains.rider.test.enums.CoreVersion
import com.jetbrains.rider.test.enums.ToolsetVersion
import com.jetbrains.rider.test.scriptingApi.callAction
import com.jetbrains.rider.test.scriptingApi.markupAdapter
import com.jetbrains.rider.test.scriptingApi.withOpenedEditor
import org.testng.annotations.Test
import withTypeProviders
import java.time.Duration

@Test
@TestEnvironment(toolset = ToolsetVersion.TOOLSET_16, coreVersion = CoreVersion.DOT_NET_CORE_3_1)
class TypeProvidersActionsTest : BaseTestWithSolution() {
    override fun getSolutionDirectoryName() = "TypeProviderLibrary"
    override val restoreNuGetPackages = true
    private val sourceFile = "TypeProviderLibrary/Caches.fs"
    private val rdFcsHost get() = project.solution.rdFSharpModel.fsharpTestHost

    private fun waitForTypeProviders() {
        waitAndPump(Lifetime.Eternal, { rdFcsHost.typeProvidersRuntimeVersion.sync(Unit) != null }, Duration.ofSeconds(60000))
    }

    @Test
    fun restartTypeProviders() {
        withTypeProviders {
            withOpenedEditor(project, sourceFile) {
                waitForDaemon()
                rdFcsHost.typeProvidersRuntimeVersion.sync(Unit).shouldNotBeNull()
                markupAdapter.hasErrors.shouldBeFalse()

                rdFcsHost.killTypeProvidersProcess.sync(Unit)
                rdFcsHost.typeProvidersRuntimeVersion.sync(Unit).shouldBeNull()

                callAction("Rider.Plugins.FSharp.RestartTypeProviders")
                waitForTypeProviders()
                waitForDaemon()
                markupAdapter.hasErrors.shouldBeFalse()
            }
        }
    }
}
